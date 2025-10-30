using Nextflow.Domain.Dtos;

namespace Nextflow.Domain.Interfaces.UseCases;

public interface IGetAllCategoryProductsUseCase
{
    Task<List<CategoryResponseDto>> Execute(Guid productId, CancellationToken ct);
}

public interface ICreateCategoryProductsUseCase
{
    Task<List<CategoryResponseDto>> Execute(Guid productId, List<Guid>? categoryIds, CancellationToken ct);
}