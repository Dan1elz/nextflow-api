using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using nextflow.Domain.Dtos;
using nextflow.Domain.Interfaces.Repositories;
using nextflow.Domain.Interfaces.UseCases.Base;
using nextflow.Domain.Models;

namespace nextflow.Application.UseCases.Products;

public class GetAllProductsUseCase(IProductRepository repository)
    : IGetAllUseCase<Product, ProductResponseDto>
{
    protected readonly IProductRepository _repository = repository;
    public async Task<ApiResponseTable<ProductResponseDto>> Execute(Expression<Func<Product, bool>> predicate, int offset, int limit, CancellationToken ct)
    {
        var data = await _repository.GetAllAsync(predicate, offset, limit, ct, x => x.Include(c => c.CategoryProducts).ThenInclude(cp => cp.Category));
        var totalItems = await _repository.CountAsync(predicate, ct);

        return new ApiResponseTable<ProductResponseDto>
        {
            Data = [.. data.Select(x => new ProductResponseDto(x))],
            TotalItems = totalItems
        };
    }
}