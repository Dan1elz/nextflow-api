using nextflow.Application.UseCases.Base;
using nextflow.Domain.Dtos;
using nextflow.Domain.Interfaces.Repositories;
using nextflow.Domain.Models;

namespace nextflow.Application.UseCases.Categories;

public class GetAllCategoriesUseCase(ICategoryRepository repository)
    : GetAllUseCaseBase<Category, ICategoryRepository, CategoryResponseDto>(repository)
{
    protected override CategoryResponseDto MapToResponseDto(Category entity) => new(entity);
}

