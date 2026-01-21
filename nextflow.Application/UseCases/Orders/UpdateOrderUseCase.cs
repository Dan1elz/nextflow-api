using Microsoft.EntityFrameworkCore;
using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Enums;
using Nextflow.Domain.Exceptions;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Interfaces.UseCases;
using Nextflow.Domain.Interfaces.UseCases.Base;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.Orders;

public class UpdateOrderUseCase(
    IOrderRepository repository,
    IOrderItemRepository orderItemRepository,
    ICreateStockMovementUseCase createStockMovement,
    IGetAllUseCase<Product, ProductResponseDto> getAllProducts
) : UpdateUseCaseBase<Order, IOrderRepository, UpdateOrderDto, OrderResponseDto>(repository)
{
    private readonly IOrderItemRepository _orderItemRepository = orderItemRepository;
    private readonly ICreateStockMovementUseCase _createStockMovement = createStockMovement;
    private readonly IGetAllUseCase<Product, ProductResponseDto> _getAllProducts = getAllProducts;

    private Dictionary<Guid, ProductResponseDto>? _productMap;
    private readonly List<CreateStockMovementDto> _stockMovementsQueue = [];

    protected override OrderResponseDto MapToResponseDto(Order entity) => new(entity);

    protected override Func<IQueryable<Order>, IQueryable<Order>>? GetInclude()
    {
        return x => x.Include(oi => oi.OrderItems).ThenInclude(p => p.Product);
    }

    protected override async Task ValidateBusinessRules(Order entity, UpdateOrderDto dto, CancellationToken ct)
    {
        if (entity.Status != OrderStatus.PendingPayment)
            throw new BadRequestException("Apenas pedidos com status 'Aguardando Pagamento' podem ser atualizados.");

        var productIds = dto.Items.Select(i => i.ProductId).Distinct().ToList();
        var products = await _getAllProducts.Execute(x => productIds.Contains(x.Id), 0, int.MaxValue, ct);

        if (products?.Data == null || products.Data.Count != productIds.Count)
            throw new BadRequestException("Um ou mais produtos não foram encontrados.");

        _productMap = products.Data.ToDictionary(p => p.Id);

        foreach (var item in dto.Items)
        {
            if (!_productMap.TryGetValue(item.ProductId, out ProductResponseDto? product))
                throw new BadRequestException($"Produto com ID {item.ProductId} não encontrado.");

            if (item.Quantity <= 0 || item.Quantity > product.Quantity)
                throw new BadRequestException($"Quantidade inválida ou estoque insuficiente para o produto {product.Name}.");
        }
    }

    protected override async Task BeforePersistence(Order entity, UpdateOrderDto dto, CancellationToken ct)
    {
        var existingItems = entity.OrderItems.ToDictionary(i => i.ProductId);

        foreach (var itemDto in dto.Items)
        {
            var product = _productMap![itemDto.ProductId];

            if (existingItems.TryGetValue(itemDto.ProductId, out OrderItem? existingItem))
            {
                if (existingItem.Quantity != itemDto.Quantity)
                {
                    var quantityDifference = itemDto.Quantity - existingItem.Quantity;
                    var movementType = quantityDifference > 0 ? MovementType.Sales : MovementType.Return;

                    _stockMovementsQueue.Add(new CreateStockMovementDto
                    {
                        ProductId = itemDto.ProductId,
                        Quantity = Math.Abs(quantityDifference),
                        MovementType = movementType,
                        Description = $"Ajuste (Update) do pedido {entity.Id}",
                        UserId = dto.UserId,
                        Quotation = product.Price
                    });

                    existingItem.Update(itemDto);
                    existingItem.SetPricing(product.Price, product.Price * itemDto.Quantity);

                    await _orderItemRepository.UpdateAsync(existingItem, ct);
                }
            }
            else
            {
                itemDto.OrderId = entity.Id;
                var newItem = new OrderItem(itemDto);
                newItem.SetPricing(product.Price, product.Price * itemDto.Quantity);

                _stockMovementsQueue.Add(new CreateStockMovementDto
                {
                    ProductId = itemDto.ProductId,
                    Quantity = itemDto.Quantity,
                    MovementType = MovementType.Sales,
                    Description = $"Adição (Update) ao pedido {entity.Id}",
                    UserId = dto.UserId,
                    Quotation = product.Price
                });

                await _orderItemRepository.AddAsync(newItem, ct);
                entity.OrderItems.Add(newItem);
            }
        }

        var removedItems = existingItems.Values
            .Where(i => !dto.Items.Any(x => x.ProductId == i.ProductId))
            .ToList();

        foreach (var removed in removedItems)
        {
            _stockMovementsQueue.Add(new CreateStockMovementDto
            {
                ProductId = removed.ProductId,
                Quantity = removed.Quantity,
                MovementType = MovementType.Return,
                Description = $"Remoção (Update) do pedido {entity.Id}",
                UserId = dto.UserId,
                Quotation = removed.UnitPrice
            });

            await _orderItemRepository.RemoveAsync(removed, ct);
            entity.OrderItems.Remove(removed);
        }

        var totalAmount = entity.OrderItems.Sum(i => i.TotalPrice);
        var totalDiscount = entity.OrderItems.Sum(i => i.Discount);
        entity.SetTotals(totalAmount, totalDiscount);
    }

    protected override async Task AfterPersistence(Order entity, UpdateOrderDto dto, CancellationToken ct)
    {
        foreach (var movementDto in _stockMovementsQueue)
        {
            await _createStockMovement.Execute(movementDto, ct);
        }
    }
}