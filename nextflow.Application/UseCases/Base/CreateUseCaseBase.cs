using nextflow.Domain.Dtos.Base;
using nextflow.Domain.Interfaces.Repositories.Base;
using nextflow.Domain.Interfaces.UseCases.Base;

namespace nextflow.Application.UseCases.Base;

public abstract class CreateUseCaseBase<TEntity, TRepository, TRequest, TResponse>(TRepository repository)
    : ICreateUseCase<TRequest, TResponse>
    where TEntity : class
    where TRepository : IBaseRepository<TEntity>
    where TRequest : BaseDto
{
    protected readonly TRepository _repository = repository;

    public virtual async Task<TResponse> Execute(TRequest dto, CancellationToken ct)
    {
        dto.Validate();
        var entity = MapToEntity(dto);
        await _repository.AddAsync(entity, ct);

        return MapToResponseDto(entity);
    }
    protected abstract TEntity MapToEntity(TRequest dto);
    protected abstract TResponse MapToResponseDto(TEntity entity);
}