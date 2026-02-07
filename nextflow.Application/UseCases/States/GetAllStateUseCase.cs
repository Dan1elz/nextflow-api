using Microsoft.EntityFrameworkCore;
using Nextflow.Application.Filters;
using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.States;

public class GetAllStatesUseCase(IStateRepository repository)
    : GetAllUseCaseBase<State, IStateRepository, StateResponseDto>(repository)
{
    protected override StateResponseDto MapToResponseDto(State entity) => new(entity);
    protected override Func<IQueryable<State>, IQueryable<State>>? GetInclude() => query => query.Include(s => s.Country);

    protected override void ApplyFilters(FilterExpressionBuilder<State> builder, FilterSet filters)
    {
        builder
            .WhereGuidEquals(filters, "countryId", s => s.CountryId)
            .WhereStringContains(filters, "CountryName", s => s.Country.Name)
            .WhereStringContains(filters, "Acronym", s => s.Acronym)
            .WhereStringContains(filters, "IbgeCode", s => s.IbgeCode)
            .WhereStringContainsAny(filters, "search", s => s.Name, s => s.Acronym);
    }
}