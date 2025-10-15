
using nextflow.Domain.Dtos;
using nextflow.Domain.Exceptions;
using nextflow.Domain.Interfaces.Repositories;
using nextflow.Domain.Interfaces.UseCases;
using nextflow.Domain.Models;

namespace nextflow.Application.UseCases.Categories;

public class CreateCategoryProductsUseCase(ICategoryProductRepository repository, ICategoryRepository categoryRepository) : ICreateCategoryProductsUseCase
{
    private readonly ICategoryProductRepository _repository = repository;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    public async Task<List<CategoryResponseDto>> Execute(Guid productId, List<Guid> categoryIds, CancellationToken ct)
    {
        var categoryProducts = await _repository.GetAllAsync(c => c.ProductId == productId, 0, int.MaxValue, ct);
        if (categoryProducts.Any()) await _repository.RemoveRangeAsync(categoryProducts, ct);

        var categories = await _categoryRepository.GetAllAsync(c => categoryIds.Contains(c.Id), 0, int.MaxValue, ct)
            ?? throw new BadRequestException("Categorias não encontradas");

        List<CategoryProduct> newCategoryProducts = [.. categories.Select(c => new CategoryProduct(new CreateCategoryProductDto
        {
            CategoryId = c.Id,
            ProductId = productId
        }))];

        await _repository.AddRangeAsync(newCategoryProducts, ct);
        return [.. newCategoryProducts.Select(c => new CategoryResponseDto(c.Category!))];

    }
}
