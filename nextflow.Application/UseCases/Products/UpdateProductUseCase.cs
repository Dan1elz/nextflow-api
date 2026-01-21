using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Interfaces.UseCases;
using Nextflow.Domain.Interfaces.Utils;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.Products;

public class UpdateProductUseCase(IProductRepository repository, IStorageService storageService, ICreateCategoryProductsUseCase createCategoryProductsUseCase)
    : UpdateUseCaseBase<Product, IProductRepository, UpdateProductDto, ProductResponseDto>(repository)
{
    protected readonly IStorageService _storageService = storageService;
    protected readonly ICreateCategoryProductsUseCase _createCategoryProductsUseCase = createCategoryProductsUseCase;
    protected override ProductResponseDto MapToResponseDto(Product entity) => new(entity);

    protected override async Task BeforePersistence(Product entity, UpdateProductDto dto, CancellationToken ct)
    {
        if (dto.Image != null)
            entity.UpdateImage(await _storageService.SaveAsync(dto.Image, ct));
        else
        {
            if (entity.Image != null)
            {
                _storageService.DeleteAsync(entity.Image!);
                entity.RemoveImage();
            }
        }
    }
    protected override async Task AfterPersistence(Product entity, UpdateProductDto dto, CancellationToken ct)
    {
        if (dto.CategoryIds != null && dto.CategoryIds.Count > 0)
            entity.CategoryProducts = [.. (await _createCategoryProductsUseCase.Execute(entity.Id, dto.CategoryIds, ct)).Select(c => new CategoryProduct(new CreateCategoryProductDto { CategoryId = c.Id, ProductId = entity.Id }))];
    }
}
