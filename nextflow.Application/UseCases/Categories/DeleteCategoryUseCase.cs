using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.Categories;

public class DeleteCategoryUseCase(ICategoryRepository repository)
    : DeleteUseCaseBase<Category, ICategoryRepository>(repository)
{ }