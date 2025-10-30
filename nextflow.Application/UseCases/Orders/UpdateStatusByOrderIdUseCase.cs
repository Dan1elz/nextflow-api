using Nextflow.Domain.Interfaces.UseCases;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Enums;
using Nextflow.Domain.Exceptions;
using Nextflow.Domain.Interfaces.Repositories;
namespace Nextflow.Application.UseCases.Orders;

public class UpdateStatusByOrderIdUseCase(IOrderRepository repository) : IUpdateStatusByOrderIdUseCase
{
    private readonly IOrderRepository _repository = repository;
    public async Task<OrderResponseDto> Execute(Guid orderId, OrderStatus status, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(orderId, ct)
            ?? throw new NotFoundException($"Pedido não encontrado com o Id: {orderId}");

        if (entity.Status == status)
            throw new BadRequestException($"O pedido já está com o status {status}.");

        if (entity.Status != OrderStatus.PendingPayment)
            throw new BadRequestException("Apenas pedidos com status 'Aguardando Pagamento' podem ser atualizados.");

        entity.Update(status);

        await _repository.UpdateAsync(entity, ct);
        return new OrderResponseDto(entity);
    }
}
