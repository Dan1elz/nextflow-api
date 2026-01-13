using Nextflow.Domain.Dtos.Base;
using Nextflow.Domain.Exceptions;
using Nextflow.Domain.Interfaces.Models;
using Nextflow.Domain.Interfaces.Repositories.Base;
using Nextflow.Domain.Interfaces.UseCases.Base;
using Nextflow.Domain.Models.Base;

namespace Nextflow.Application.UseCases.Base;

public abstract class UpdateUseCaseBase<TEntity, TRepository, TRequest, TResponse>(TRepository repository)
    : IUpdateUseCase<TRequest, TResponse>
    where TEntity : BaseModel, IUpdatable<TRequest>
    where TRepository : IBaseRepository<TEntity>
    where TRequest : BaseDto
{
    protected readonly TRepository _repository = repository;

    public virtual async Task<TResponse> Execute(Guid id, TRequest dto, CancellationToken ct)
    {
        dto.Validate();
        var entity = await _repository.GetByIdAsync(id, ct);

        if (entity == null)
            throw new NotFoundException($"{entity?.Singular} com id {id} não encontrad{entity?.Preposition}.");

        if (!entity.IsActive)
            throw new BadRequestException($"{entity.Singular} já está inativ{entity.Preposition}/cancelad{entity.Preposition}.");

        entity.Update(dto);
        await _repository.UpdateAsync(entity, ct);

        return MapToResponseDto(entity);
    }

    protected abstract TResponse MapToResponseDto(TEntity entity);
}