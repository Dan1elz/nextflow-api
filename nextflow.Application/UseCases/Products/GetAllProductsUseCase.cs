using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Interfaces.UseCases.Base;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.Products;

public class GetAllProductsUseCase(IProductRepository repository)
    : IGetAllUseCase<Product, ProductResponseDto>
{
    protected readonly IProductRepository _repository = repository;
    public async Task<ApiResponseTable<ProductResponseDto>> Execute(Expression<Func<Product, bool>> predicate, int offset, int limit, CancellationToken ct)
    {
        var data = await _repository.GetAllAsync(predicate, offset, limit, ct, x => x.Include(s => s.Supplier).Include(c => c.CategoryProducts).ThenInclude(cp => cp.Category));
        var totalItems = await _repository.CountAsync(predicate, ct);

        return new ApiResponseTable<ProductResponseDto>
        {
            Data = [.. data.Select(x => new ProductResponseDto(x))],
            TotalItems = totalItems
        };
    }
}