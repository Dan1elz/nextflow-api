using nextflow.Domain.Dtos;
using nextflow.Domain.Interfaces.Repositories;
using nextflow.Domain.Interfaces.UseCases;
using nextflow.Domain.Interfaces.UseCases.Base;
using nextflow.Domain.Interfaces.Utils;
using nextflow.Domain.Models;

namespace nextflow.Application.UseCases.Products;

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
