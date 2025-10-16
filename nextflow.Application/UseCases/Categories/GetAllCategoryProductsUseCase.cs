using Microsoft.EntityFrameworkCore;
using nextflow.Domain.Dtos;
using nextflow.Domain.Interfaces.Repositories;
using nextflow.Domain.Interfaces.UseCases;

namespace nextflow.Application.UseCases.Categories;

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
