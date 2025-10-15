using nextflow.Domain.Dtos;

namespace nextflow.Domain.Interfaces.UseCases;

public interface ICreateStockMovementUseCase
{
    Task<StockMovementResponseDto> Execute(CreateStockMovementDto dto, CancellationToken ct);
}