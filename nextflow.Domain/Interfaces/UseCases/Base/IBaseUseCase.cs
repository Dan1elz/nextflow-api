using System.Linq.Expressions;
using nextflow.Domain.Dtos;

namespace nextflow.Domain.Interfaces.UseCases.Base;

public interface ICreateUseCase<TRequest, TResponse>
{
    Task<TResponse> Execute(TRequest dto, CancellationToken ct);
}

public interface IUpdateUseCase<TRequest, TResponse>
{
    Task<TResponse> Execute(Guid id, TRequest dto, CancellationToken ct);
}

public interface IDeleteUseCase
{
    Task Execute(Guid id, CancellationToken ct);
}

public interface IGetByIdUseCase<TResponse>
{
    Task<TResponse> Execute(Guid id, CancellationToken ct);
}

public interface IGetAllUseCase<TEntity, TResponse>
{
    Task<ApiResponseTable<TResponse>> Execute(Expression<Func<TEntity, bool>> predicate, int offset, int limit, CancellationToken ct);
}
