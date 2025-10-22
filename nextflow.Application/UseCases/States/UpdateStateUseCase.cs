using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.States;

public class UpdateStateUseCase(IStateRepository repository)
    : UpdateUseCaseBase<State, IStateRepository, UpdateStateDto, StateResponseDto>(repository)
{
    protected override StateResponseDto MapToResponseDto(State entity) => new(entity);
}
