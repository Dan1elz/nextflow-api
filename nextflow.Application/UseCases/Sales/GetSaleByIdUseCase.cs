using Microsoft.EntityFrameworkCore;
using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.Sales;

public class GetSaleByIdUseCase(ISaleRepository repository) : GetByIdUseCaseBase<Sale, ISaleRepository, SaleResponseDto>(repository)
{
    protected override SaleResponseDto MapToResponseDto(Sale entity) => new(entity);
    protected override Func<IQueryable<Sale>, IQueryable<Sale>>? GetInclude() => query => query.Include(city => city.Payments);
}
