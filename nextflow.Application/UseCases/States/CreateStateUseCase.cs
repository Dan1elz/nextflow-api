using nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.States;

public class CreateStateUseCase(IStateRepository repository)
    : CreateUseCaseBase<State, IStateRepository, CreateStateDto, StateResponseDto>(repository)
{
    protected override State MapToEntity(CreateStateDto dto) => new(dto);
    protected override StateResponseDto MapToResponseDto(State entity) => new(entity);
}

