using nextflow.Domain.Exceptions;
using nextflow.Domain.Interfaces.Repositories.Base;
using nextflow.Domain.Interfaces.UseCases.Base;
using nextflow.Domain.Models.Base;

namespace nextflow.Application.UseCases.Base;

public abstract class DeleteUseCaseBase<TEntity, TRepository>(TRepository repository)
    : IDeleteUseCase
    where TEntity : BaseModel
    where TRepository : IBaseRepository<TEntity>
{
    protected readonly TRepository _repository = repository;

    public virtual async Task Execute(Guid id, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(id, ct)
            ?? throw new NotFoundException($"{typeof(TEntity).Name} com id {id} não encontrado.");

        entity.Delete();

        await _repository.UpdateAsync(entity, ct);
    }
}