using Microsoft.EntityFrameworkCore;
using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.Products;

public class GetProductByIdUseCase(IProductRepository repository) : GetByIdUseCaseBase<Product, IProductRepository, ProductResponseDto>(repository)
{
    protected override ProductResponseDto MapToResponseDto(Product entity) => new(entity);
    protected override Func<IQueryable<Product>, IQueryable<Product>>? GetInclude() => query => query
        .Include(c => c.CategoryProducts)
            .ThenInclude(cp => cp.Category);
}
