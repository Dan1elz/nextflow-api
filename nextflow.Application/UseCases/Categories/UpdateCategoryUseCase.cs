using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.Categories;

public class UpdateCategoryUseCase(ICategoryRepository repository)
    : UpdateUseCaseBase<Category, ICategoryRepository, UpdateCategoryDto, CategoryResponseDto>(repository)
{
    protected override CategoryResponseDto MapToResponseDto(Category entity) => new(entity);
}