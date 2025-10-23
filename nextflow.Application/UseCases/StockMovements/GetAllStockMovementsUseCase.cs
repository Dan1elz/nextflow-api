using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Interfaces.UseCases.Base;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.StockMovements;

public class GetAllStockMovementsUseCase(IStockMovementRepository repository)
    : IGetAllUseCase<StockMovement, StockMovementResponseDto>
{
    protected readonly IStockMovementRepository _repository = repository;
    public async Task<ApiResponseTable<StockMovementResponseDto>> Execute(Expression<Func<StockMovement, bool>> predicate, int offset, int limit, CancellationToken ct)
    {
        var data = await _repository.GetAllAsync(predicate, offset, limit, ct, x => x.Include(u => u.User).Include(p => p.Product));
        var totalItems = await _repository.CountAsync(predicate, ct);

        return new ApiResponseTable<StockMovementResponseDto>
        {
            Data = [.. data.Select(x => new StockMovementResponseDto(x))],
            TotalItems = totalItems
        };
    }
}