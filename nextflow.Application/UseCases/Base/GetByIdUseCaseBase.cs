using Nextflow.Domain.Exceptions;
using Nextflow.Domain.Interfaces.Repositories.Base;
using Nextflow.Domain.Interfaces.UseCases.Base;

namespace Nextflow.Application.UseCases.Base;

public abstract class GetByIdUseCaseBase<TEntity, TRepository, TResponse>(TRepository repository)
    : IGetByIdUseCase<TResponse>
    where TEntity : class
    where TRepository : IBaseRepository<TEntity>
{
    protected readonly TRepository _repository = repository;

    public virtual async Task<TResponse> Execute(Guid id, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(id, ct) ?? throw new NotFoundException($"Entidade não encontrada com o Id: {id}");

        return MapToResponseDto(entity);
    }

    protected abstract TResponse MapToResponseDto(TEntity entity);
}