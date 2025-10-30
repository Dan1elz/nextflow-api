using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Interfaces.UseCases.Base;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.Sales;

public class GetAllSalesUseCase(ISaleRepository repository) : IGetAllUseCase<Sale, SaleResponseDto>
{
    public async Task<ApiResponseTable<SaleResponseDto>> Execute(Expression<Func<Sale, bool>> predicate, int offset, int limit, CancellationToken ct)
    {
        var data = await repository.GetAllAsync(predicate, offset, limit, ct, x => x.Include(s => s.Payments));
        var totalItems = await repository.CountAsync(predicate, ct);

        return new ApiResponseTable<SaleResponseDto>
        {
            Data = [.. data.Select(x => new SaleResponseDto(x))],
            TotalItems = totalItems
        };
    }
}