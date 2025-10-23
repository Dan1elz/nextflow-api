using nextflow.Domain.Interfaces.UseCases;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Enums;
using Nextflow.Domain.Exceptions;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Interfaces.UseCases;
using Nextflow.Domain.Interfaces.UseCases.Base;
using Nextflow.Domain.Models;

namespace nextflow.Application.UseCases.Orders;

public class CreateOrderUseCase(
    IOrderRepository repository,
    IOrderItemRepository orderItemRepository,
    IGetAllUseCase<Product, ProductResponseDto> getAllProducts,
    ICreateStockMovementUseCase createStockMovement
    )
    : ICreateOrderUseCase
{
    private readonly IOrderRepository _repository = repository;
    private readonly IOrderItemRepository _orderItemRepository = orderItemRepository;
    public readonly IGetAllUseCase<Product, ProductResponseDto> _getAllProducts = getAllProducts;
    private readonly ICreateStockMovementUseCase _createStockMovement = createStockMovement;

    public async Task<OrderResponseDto> Execute(CreateOrderDto dto, CancellationToken ct)
    {
        dto.Validate();
        var productsId = dto.Items.Select(i => i.ProductId).ToList();
        var products = await _getAllProducts.Execute(x => productsId.Contains(x.Id), 0, int.MaxValue, ct);

        if (products?.Data == null || products.Data.Count == 0)
            throw new BadRequestException("Nenhum produto válido foi encontrado.");

        var productMap = products.Data.ToDictionary(p => p.Id);


        foreach (var item in dto.Items)
        {
            if (!productMap.TryGetValue(item.ProductId, out ProductResponseDto? product))
                throw new BadRequestException($"Produto com ID {item.ProductId} não encontrado.");
            if (item.Quantity <= 0 || item.Quantity > product.Quantity)
                throw new BadRequestException($"Quantidade inválida para o produto {product.Name}.");

        }
        //encontrar o total da compra e total de desconto
        decimal totalAmount = 0;
        decimal totalDiscount = 0;

        //Criar a Ordem de pedido
        Order order = new(dto);

        foreach (var item in dto.Items)
        {
            item.OrderId = order.Id;
            //criar o item da ordem de pedido
            OrderItem orderItem = new(item);
            productMap.TryGetValue(item.ProductId, out ProductResponseDto? product);

            var unitPrice = product!.Price;
            var totalPrice = unitPrice * item.Quantity;
            totalAmount += totalPrice;
            totalDiscount += item.Discount;


            orderItem.SetPricing(unitPrice, totalPrice);

            //efetuar a movimentação de estoque para cada um.
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

        order.SetTotals(totalAmount, totalDiscount);
        await _repository.AddAsync(order, ct);
        return new OrderResponseDto(order);
    }
}