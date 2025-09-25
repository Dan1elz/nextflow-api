using System.Linq.Expressions;
using nextflow.Domain.Dtos;
using nextflow.Domain.Interfaces.Repositories.Base;
using nextflow.Domain.Interfaces.UseCases.Base;

namespace nextflow.Application.UseCases.Base;

public abstract class GetAllUseCase<TEntity, TRepository, TResponse>(TRepository repository)
    : IGetAllUseCase<TEntity, TResponse>
    where TEntity : class
    where TRepository : IBaseRepository<TEntity>
{
    protected readonly TRepository _repository = repository;
    public async Task<ApiResponseTable<TResponse>> Execute(Expression<Func<TEntity, bool>> predicate, int offset, int limit, CancellationToken ct)
    {
        var data = await _repository.GetAllAsync(predicate, offset, limit, ct);
        var totalItems = await _repository.CountAsync(predicate, ct);

        return new ApiResponseTable<TResponse>
        {
            Data = [.. data.Select(MapToResponseDto)],
            TotalItems = totalItems
        };
    }

    protected abstract TResponse MapToResponseDto(TEntity entity);
}
