using nextflow.Domain.Dtos.Base;
using nextflow.Domain.Exceptions;
using nextflow.Domain.Interfaces.Models;
using nextflow.Domain.Interfaces.Repositories.Base;
using nextflow.Domain.Interfaces.UseCases.Base;

namespace nextflow.Application.UseCases.Base;

public abstract class UpdateUseCaseBase<TEntity, TRepository, TRequest, TResponse>(TRepository repository)
    : IUpdateUseCase<TRequest, TResponse>
    where TEntity : class, IUpdatable<TRequest>
    where TRepository : IBaseRepository<TEntity>
    where TRequest : BaseDto
{
    protected readonly TRepository _repository = repository;

    public virtual async Task<TResponse> Execute(Guid id, TRequest dto, CancellationToken ct)
    {
        dto.Validate();
        var entity = await _repository.GetByIdAsync(id, ct)
            ?? throw new NotFoundException($"{typeof(TEntity).Name} com id {id} não encontrado.");

        entity.Update(dto);
        await _repository.UpdateAsync(entity, ct);

        return MapToResponseDto(entity);
    }

    protected abstract TResponse MapToResponseDto(TEntity entity);
}