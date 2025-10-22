using Microsoft.EntityFrameworkCore;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Interfaces.UseCases;

namespace Nextflow.Application.UseCases.Categories;

public class GetAllCategoriesByProductUseCase
    (ICategoryProductRepository repository) : IGetAllCategoryProductsUseCase
{
    private readonly ICategoryProductRepository _repository = repository;
    public async Task<List<CategoryResponseDto>> Execute(Guid productId, CancellationToken ct)
    {
        var categories = await _repository.GetAllAsync(c => c.ProductId == productId, 0, int.MaxValue, ct, x => x.Include(c => c.Category));

        return [.. categories
            .Where(c => c.Category != null)
            .Select(c => new CategoryResponseDto(c.Category!))];
    }
}
