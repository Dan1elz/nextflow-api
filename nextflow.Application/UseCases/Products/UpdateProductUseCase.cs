using Nextflow.Domain.Dtos;
using Nextflow.Domain.Exceptions;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Interfaces.UseCases;
using Nextflow.Domain.Interfaces.UseCases.Base;
using Nextflow.Domain.Interfaces.Utils;

namespace Nextflow.Application.UseCases.Products
{
    public class UpdateProductUseCase(
        IProductRepository repository,
        IStorageService storageService,
        ICreateCategoryProductsUseCase createCategoryProductsUseCase
    ) : IUpdateUseCase<UpdateProductDto, ProductResponseDto>
    {
        protected readonly IProductRepository _repository = repository;
        protected readonly IStorageService _storageService = storageService;
        protected readonly ICreateCategoryProductsUseCase _createCategoryProductsUseCase = createCategoryProductsUseCase;

        public virtual async Task<ProductResponseDto> Execute(Guid id, UpdateProductDto dto, CancellationToken ct)
        {
            dto.Validate();
            var entity = await _repository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException($"Produto com id {id} não encontrado.");

            entity.Update(dto);

            if (dto.Image != null)
                entity.UpdateImage(await _storageService.SaveAsync(dto.Image, ct));
            else
            {
                _storageService.DeleteAsync(entity.Image!);
                entity.RemoveImage();
            }

            await _repository.UpdateAsync(entity, ct);

            var response = new ProductResponseDto(entity)
            {
                Categories = await _createCategoryProductsUseCase.Execute(entity.Id, dto.CategoryIds, ct)
            };

            return response;
        }
    }
}