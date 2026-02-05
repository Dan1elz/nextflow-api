using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
using Nextflow.Application.Filters;
using Nextflow.Application.Utils;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories.Base;
using Nextflow.Domain.Interfaces.UseCases.Base;

namespace Nextflow.Application.UseCases.Base;

public abstract class GetAllUseCaseBase<TEntity, TRepository, TResponse>(TRepository repository)
    : IGetAllUseCase<TEntity, TResponse>
    where TEntity : class
    where TRepository : IBaseRepository<TEntity>
{
    protected readonly TRepository _repository = repository;

    public async Task<ApiResponseTable<TResponse>> Execute(int offset, int limit, IReadOnlyDictionary<string, string>? filters, CancellationToken ct)
    {
        var predicate = BuildPredicate(filters);
        var includeExpression = GetInclude();

        var data = await _repository.GetAllAsync(predicate, offset, limit, ct, includeExpression);
        var totalItems = await _repository.CountAsync(predicate, ct);

        return new ApiResponseTable<TResponse>
        {
            Data = [.. data.Select(MapToResponseDto)],
            TotalItems = totalItems
        };
    }

    private Expression<Func<TEntity, bool>> BuildPredicate(IReadOnlyDictionary<string, string>? filters)
    {
        var predicate = BasePredicate;

        var filterSet = new FilterSet(filters);
        if (!filterSet.IsEmpty)
        {
            var builder = new FilterExpressionBuilder<TEntity>();
            ApplyCommonFilters(builder, filterSet);
            ApplyFilters(builder, filterSet);

            if (builder.HasAny)
                predicate = predicate.AndAlso(builder.Predicate);
        }

        return predicate;
    }

    protected virtual Expression<Func<TEntity, bool>> BasePredicate => _ => true;

    /// <summary>
    /// Registre aqui as regras de filtro específicas da entidade.
    /// </summary>
    protected virtual void ApplyFilters(FilterExpressionBuilder<TEntity> builder, FilterSet filters) { }

    /// <summary>
    /// Filtros "globais" suportados quando a entidade possui as propriedades correspondentes.
    /// </summary>
    protected virtual void ApplyCommonFilters(FilterExpressionBuilder<TEntity> builder, FilterSet filters)
    {
        // isActive=true|false
        if (filters.TryGetString("isActive", out var raw) && FilterValueParsers.TryParseBool(raw, out var b))
        {
            var prop = typeof(TEntity).GetProperty("IsActive");
            if (prop?.PropertyType == typeof(bool))
            {
                var p = Expression.Parameter(typeof(TEntity), "e");
                var left = Expression.Property(p, prop);
                var right = Expression.Constant(b, typeof(bool));
                var body = Expression.Equal(left, right);
                builder.And(Expression.Lambda<Func<TEntity, bool>>(body, p));
            }
        }
    }

    protected abstract TResponse MapToResponseDto(TEntity entity);
    protected virtual Func<IQueryable<TEntity>, IQueryable<TEntity>>? GetInclude() => null;
}