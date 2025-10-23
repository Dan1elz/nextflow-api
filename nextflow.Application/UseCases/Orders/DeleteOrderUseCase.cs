using Microsoft.EntityFrameworkCore;
using nextflow.Domain.Interfaces.UseCases;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Enums;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Interfaces.UseCases;

namespace nextflow.Application.UseCases.Orders;

public class DeleteOrderUseCase(
    IOrderRepository repository,
    ICreateStockMovementUseCase createStockMovement

    ) : IDeleteOrderUseCase
{
    private readonly IOrderRepository _repository = repository;
    private readonly ICreateStockMovementUseCase _createStockMovement = createStockMovement;
    public async Task Execute(Guid id, Guid userId, CancellationToken ct)
    {
        var order = await _repository.GetByIdAsync(id, ct, x => x.Include(o => o.OrderItems))
            ?? throw new Exception("Pedido não encontrado.");

        if (!order.IsActive)
            throw new Exception("Pedido já está cancelado.");

        order.Delete();

        var orderItems = order.OrderItems;
        foreach (var item in orderItems)
        {
            var stockMovementDto = new CreateStockMovementDto
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                MovementType = MovementType.Return,
                Description = $"Estorno do pedido {order.Id}",
                UserId = userId
            };

            await _createStockMovement.Execute(stockMovementDto, ct);
        }

        await _repository.UpdateAsync(order, ct);
    }
}