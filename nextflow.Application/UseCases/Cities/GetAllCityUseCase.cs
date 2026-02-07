using Microsoft.EntityFrameworkCore;
using Nextflow.Application.Filters;
using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.Cities;

public class GetAllCityUseCase(ICityRepository repository)
    : GetAllUseCaseBase<City, ICityRepository, CityResponseDto>(repository)
{
    protected override CityResponseDto MapToResponseDto(City entity) => new(entity);
    protected override Func<IQueryable<City>, IQueryable<City>>? GetInclude() => query => query.Include(city => city.State);

    protected override void ApplyFilters(FilterExpressionBuilder<City> builder, FilterSet filters)
    {
        builder
            .WhereGuidEquals(filters, "stateId", c => c.StateId)
            .WhereStringContains(filters, "search", c => c.Name)
            .WhereStringContains(filters, "ibgeCode", c => c.IbgeCode);
    }
}