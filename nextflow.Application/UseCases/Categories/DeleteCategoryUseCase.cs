using nextflow.Application.UseCases.Base;
using nextflow.Domain.Interfaces.Repositories;
using nextflow.Domain.Models;

namespace nextflow.Application.UseCases.Categories;

public class DeleteCategoryUseCase(ICategoryRepository repository)
    : DeleteUseCaseBase<Category, ICategoryRepository>(repository)
{ }