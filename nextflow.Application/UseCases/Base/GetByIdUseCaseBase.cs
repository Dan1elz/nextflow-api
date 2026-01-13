using Nextflow.Domain.Exceptions;
using Nextflow.Domain.Interfaces.Repositories.Base;
using Nextflow.Domain.Interfaces.UseCases.Base;
using Nextflow.Domain.Models.Base;

namespace Nextflow.Application.UseCases.Base;

public abstract class GetByIdUseCaseBase<TEntity, TRepository, TResponse>(TRepository repository)
    : IGetByIdUseCase<TResponse>
    where TEntity : BaseModel
    where TRepository : IBaseRepository<TEntity>
{
    protected readonly TRepository _repository = repository;

    public virtual async Task<TResponse> Execute(Guid id, CancellationToken ct)
    {
        var includeExpression = GetInclude();

        var entity = await _repository.GetByIdAsync(id, ct, includeExpression);

        if (entity == null)
            throw new NotFoundException($"{entity?.Singular} com id {id} não encontrad{entity?.Preposition}.");

        return MapToResponseDto(entity);
    }

    protected abstract TResponse MapToResponseDto(TEntity entity);

    protected virtual Func<IQueryable<TEntity>, IQueryable<TEntity>>? GetInclude() => null;
}