using Microsoft.EntityFrameworkCore;
using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.States;

public class GetAllStatesUseCase(IStateRepository repository)
    : GetAllUseCaseBase<State, IStateRepository, StateResponseDto>(repository)
{
    protected override StateResponseDto MapToResponseDto(State entity) => new(entity);
    protected override Func<IQueryable<State>, IQueryable<State>>? GetInclude() => query => query.Include(city => city.Country);
}