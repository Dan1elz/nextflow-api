using System.Linq.Expressions;
using Nextflow.Domain.Dtos;

namespace Nextflow.Domain.Interfaces.UseCases.Base;

public interface ICreateUseCase<TRequest, TResponse>
{
    Task<TResponse> Execute(TRequest dto, CancellationToken ct);
}

public interface IUpdateUseCase<TRequest, TResponse>
{
    Task<TResponse> Execute(Guid id, TRequest dto, CancellationToken ct);
}

public interface IDeleteUseCase<TEntity>
{
    Task Execute(Guid id, CancellationToken ct, Guid? userId = null);
}

public interface IGetByIdUseCase<TResponse>
{
    Task<TResponse> Execute(Guid id, CancellationToken ct);
}

public interface IGetAllUseCase<TEntity, TResponse>
{
    Task<ApiResponseTable<TResponse>> Execute(Expression<Func<TEntity, bool>> predicate, int offset, int limit, CancellationToken ct);
}

public interface IReactivateUseCase<TEntity>
{
    Task Execute(Guid id, CancellationToken ct, Guid? userId = null);
}