using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.StockMovements;

public class GetStockMovementByIdUseCase(IStockMovementRepository repository)
    : GetByIdUseCaseBase<StockMovement, IStockMovementRepository, StockMovementResponseDto>(repository)
{
    protected override StockMovementResponseDto MapToResponseDto(StockMovement entity) => new(entity);
}
