using nextflow.Application.UseCases.Base;
using nextflow.Domain.Dtos;
using nextflow.Domain.Interfaces.Repositories;
using nextflow.Domain.Models;

namespace nextflow.Application.UseCases.StockMovements;

public class GetAllStockMovementsUseCase(IStockMovementRepository repository)
    : GetAllUseCaseBase<StockMovement, IStockMovementRepository, StockMovementResponseDto>(repository)
{
    protected override StockMovementResponseDto MapToResponseDto(StockMovement entity) => new(entity);
}

