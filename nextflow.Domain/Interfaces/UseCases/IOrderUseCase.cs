using Nextflow.Domain.Dtos;
using Nextflow.Domain.Enums;

namespace Nextflow.Domain.Interfaces.UseCases;

public interface ICreateOrderUseCase
{
    Task<OrderResponseDto> Execute(CreateOrderDto dto, CancellationToken ct);
}

public interface IDeleteOrderUseCase
{
    Task Execute(Guid id, Guid userId, CancellationToken ct);
}

public interface IUpdateStatusByOrderIdUseCase
{
    Task<OrderResponseDto> Execute(Guid orderId, Guid userId, OrderStatus status, CancellationToken ct);
}