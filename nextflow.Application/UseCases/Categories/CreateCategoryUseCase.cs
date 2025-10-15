using nextflow.Application.UseCases.Base;
using nextflow.Domain.Dtos;
using nextflow.Domain.Interfaces.Repositories;
using nextflow.Domain.Models;

namespace nextflow.Application.UseCases.Categories;

public class CreateCategoryUseCase(ICategoryRepository repository)
    : CreateUseCaseBase<Category, ICategoryRepository, CreateCategoryDto, CategoryResponseDto>(repository)
{
    protected override Category MapToEntity(CreateCategoryDto dto) => new(dto);
    protected override CategoryResponseDto MapToResponseDto(Category entity) => new(entity);

}

