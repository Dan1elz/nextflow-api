using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Interfaces.UseCases;
using Nextflow.Domain.Interfaces.UseCases.Base;
using Nextflow.Domain.Interfaces.Utils;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.Products;

public class CreateProductUseCase(IProductRepository repository, IStorageService storageService, ICreateCategoryProductsUseCase createCategoryProductsUseCase)
    : ICreateUseCase<CreateProductDto, ProductResponseDto>
{
    protected readonly IProductRepository _repository = repository;
    protected readonly IStorageService _storageService = storageService;
    protected readonly ICreateCategoryProductsUseCase _createCategoryProductsUseCase = createCategoryProductsUseCase;

    public virtual async Task<ProductResponseDto> Execute(CreateProductDto dto, CancellationToken ct)
    {
        dto.Validate();
        var entity = new Product(dto);

        if (dto.Image != null)
            entity.UpdateImage(await _storageService.SaveAsync(dto.Image, ct));

        await _repository.AddAsync(entity, ct);
        var response = new ProductResponseDto(entity)
        {
            Categories = await _createCategoryProductsUseCase.Execute(entity.Id, dto.CategoryIds, ct)
        };

        return response;
    }
}
