using Microsoft.EntityFrameworkCore;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Enums;
using Nextflow.Domain.Exceptions;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Interfaces.UseCases;
using Nextflow.Domain.Interfaces.UseCases.Base;
using Nextflow.Domain.Models;

namespace nextflow.Application.UseCases.Orders;

public class UpdateOrderUseCase(
    IOrderRepository repository,
    IOrderItemRepository orderItemRepository,
    ICreateStockMovementUseCase createStockMovement,
    IGetAllUseCase<Product, ProductResponseDto> getAllProducts
) : IUpdateUseCase<UpdateOrderDto, OrderResponseDto>
{
    private readonly IOrderRepository _repository = repository;
    private readonly IOrderItemRepository _orderItemRepository = orderItemRepository;
    private readonly IGetAllUseCase<Product, ProductResponseDto> _getAllProducts = getAllProducts;
    private readonly ICreateStockMovementUseCase _createStockMovement = createStockMovement;

    public async Task<OrderResponseDto> Execute(Guid id, UpdateOrderDto dto, CancellationToken ct)
    {
        // 🔹 Buscar pedido com itens e produtos
        var order = await _repository.GetByIdAsync(
            id, ct,
            x => x.Include(oi => oi.OrderItems).ThenInclude(p => p.Product))
            ?? throw new NotFoundException($"Pedido não encontrado com o Id: {id}");

        if (!order.IsActive)
            throw new BadRequestException("Não é possível atualizar um pedido cancelado.");

        if (order.Status != OrderStatus.PendingPayment)
            throw new BadRequestException("Apenas pedidos com status 'Aguardando Pagamento' podem ser atualizados.");

        // 🔹 Validar produtos informados
        var productIds = dto.Items.Select(i => i.ProductId).ToList();
        var products = await _getAllProducts.Execute(x => productIds.Contains(x.Id), 0, int.MaxValue, ct);

        if (products?.Data == null || products.Data.Count == 0)
            throw new BadRequestException("Nenhum produto válido foi encontrado.");

        var productMap = products.Data.ToDictionary(p => p.Id);
        var existingItems = order.OrderItems.ToDictionary(i => i.ProductId);

        // 🔹 Atualizar / adicionar itens
        foreach (var item in dto.Items)
        {
            if (!productMap.TryGetValue(item.ProductId, out ProductResponseDto? product))
                throw new BadRequestException($"Produto com ID {item.ProductId} não encontrado.");

            if (item.Quantity <= 0 || item.Quantity > product.Quantity)
                throw new BadRequestException($"Quantidade inválida para o produto {product.Name}.");

            if (existingItems.TryGetValue(item.ProductId, out OrderItem? existingItem))
            {
                // Item já existe — verificar se precisa ajustar a quantidade
                if (existingItem.Quantity != item.Quantity)
                {
                    var quantityDifference = item.Quantity - existingItem.Quantity;

                    var movementType = quantityDifference > 0 ? MovementType.Sales : MovementType.Return;

                    // Criar movimentação antes de atualizar o item (para pegar quantidade antiga corretamente)
                    await _createStockMovement.Execute(new CreateStockMovementDto
                    {
                        ProductId = item.ProductId,
                        Quantity = Math.Abs(quantityDifference),
                        MovementType = movementType,
                        Description = $"Ajuste do pedido {order.Id}",
                        UserId = dto.UserId,
                        Quotation = product.Price
                    }, ct);

                    // Atualizar o item no pedido
                    existingItem.Update(item);
                    existingItem.SetPricing(product.Price, product.Price * item.Quantity);

                    await _orderItemRepository.UpdateAsync(existingItem, ct);
                }
            }
            else
            {
                // Novo item — criar e adicionar à ordem
                item.OrderId = order.Id;
                var orderItem = new OrderItem(item);

                var unitPrice = product!.Price;
                var totalPrice = unitPrice * item.Quantity;

                orderItem.SetPricing(unitPrice, totalPrice);

                await _createStockMovement.Execute(new CreateStockMovementDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    MovementType = MovementType.Sales,
                    Description = $"Movimentação de estoque para o pedido {order.Id}",
                    UserId = dto.UserId,
                    Quotation = product.Price
                }, ct);

                await _orderItemRepository.AddAsync(orderItem, ct);
                order.OrderItems.Add(orderItem);
            }
        }

        // 🔹 Remover itens que não estão mais na requisição
        var removedItems = existingItems.Values
            .Where(i => !dto.Items.Any(x => x.ProductId == i.ProductId))
            .ToList();

        foreach (var removed in removedItems)
        {
            await _createStockMovement.Execute(new CreateStockMovementDto
            {
                ProductId = removed.ProductId,
                Quantity = removed.Quantity,
                MovementType = MovementType.Return,
                Description = $"Remoção de item do pedido {order.Id}",
                UserId = dto.UserId,
                Quotation = removed.UnitPrice
            }, ct);

            await _orderItemRepository.RemoveAsync(removed, ct);
            order.OrderItems.Remove(removed);
        }

        // 🔹 Recalcular totais da ordem
        var totalAmount = order.OrderItems.Sum(i => i.TotalPrice);
        var totalDiscount = order.OrderItems.Sum(i => i.Discount);
        order.SetTotals(totalAmount, totalDiscount);

        // 🔹 Atualizar pedido principal
        await _repository.UpdateAsync(order, ct);

        return new OrderResponseDto(order);
    }
}
