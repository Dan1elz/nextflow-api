using Nextflow.Domain.Interfaces.UseCases;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Enums;
using Nextflow.Domain.Exceptions;
using Nextflow.Domain.Interfaces.Repositories;
namespace Nextflow.Application.UseCases.Orders;

public class UpdateStatusByOrderIdUseCase(IOrderRepository repository, ICreateStockMovementUseCase createStockMovementUseCase) : IUpdateStatusByOrderIdUseCase
{
    private readonly IOrderRepository _repository = repository;
    private readonly ICreateStockMovementUseCase _createStockMovementUseCase = createStockMovementUseCase;

    public async Task<OrderResponseDto> Execute(Guid orderId, Guid userId, OrderStatus status, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(orderId, ct)
            ?? throw new NotFoundException($"Pedido não encontrado com o Id: {orderId}");

        if (entity.Status == status)
            throw new BadRequestException($"O pedido já está com o status {status}.");

        // 🔒 Regra 1 — Permitir mudar de Pendente para Pago
        if (entity.Status == OrderStatus.PendingPayment && status == OrderStatus.PaymentConfirmed)
        {
            entity.UpdateStatus(status);
            await _repository.UpdateAsync(entity, ct);

            return new OrderResponseDto(entity);
        }

        // 🔒 Regra 2 — Permitir reestorno de pedidos pagos em até 7 dias
        if (entity.Status == OrderStatus.PaymentConfirmed && status == OrderStatus.Returned)
        {
            if (!entity.UpdateAt.HasValue)
                throw new BadRequestException("Data de pagamento não informada para este pedido.");

            var daysSincePayment = (DateTime.UtcNow - entity.UpdateAt.Value).TotalDays;

            if (daysSincePayment > 7)
                throw new BadRequestException("O pedido só pode ser retornado até 7 dias após o pagamento.");

            foreach (var item in entity.OrderItems)
            {
                await _createStockMovementUseCase.Execute(new CreateStockMovementDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    MovementType = MovementType.Return,
                    Description = $"Estorno do pedido {entity.Id}",
                    UserId = userId
                }, ct);
            }
            entity.UpdateStatus(status);
            await _repository.UpdateAsync(entity, ct);

            return new OrderResponseDto(entity);
        }
        throw new BadRequestException($"Não é possível alterar o status de {entity.Status} para {status}.");
    }
}
