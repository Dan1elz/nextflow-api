using Microsoft.EntityFrameworkCore;
using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.StockMovements;

public class GetAllStockMovementsUseCase(IStockMovementRepository repository)
    : GetAllUseCaseBase<StockMovement, IStockMovementRepository, StockMovementResponseDto>(repository)
{
    protected override StockMovementResponseDto MapToResponseDto(StockMovement entity) => new(entity);
    protected override Func<IQueryable<StockMovement>, IQueryable<StockMovement>>? GetInclude() => query => query.Include(u => u.User).Include(p => p.Product);
}