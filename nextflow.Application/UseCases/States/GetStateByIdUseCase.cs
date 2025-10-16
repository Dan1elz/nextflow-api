using nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.States;

public class GetStateByIdUseCase(IStateRepository repository)
    : GetByIdUseCaseBase<State, IStateRepository, StateResponseDto>(repository)
{
    protected override StateResponseDto MapToResponseDto(State entity) => new(entity);
}

