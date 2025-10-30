using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.Categories;

public class CreateCategoryUseCase(ICategoryRepository repository)
    : CreateUseCaseBase<Category, ICategoryRepository, CreateCategoryDto, CategoryResponseDto>(repository)
{
    protected override Category MapToEntity(CreateCategoryDto dto) => new(dto);
    protected override CategoryResponseDto MapToResponseDto(Category entity) => new(entity);

}

