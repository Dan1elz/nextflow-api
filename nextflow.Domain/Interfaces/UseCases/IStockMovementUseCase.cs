using Nextflow.Domain.Dtos;

namespace Nextflow.Domain.Interfaces.UseCases;

public interface ICreateStockMovementUseCase
{
    Task<StockMovementResponseDto> Execute(CreateStockMovementDto dto, CancellationToken ct);
}