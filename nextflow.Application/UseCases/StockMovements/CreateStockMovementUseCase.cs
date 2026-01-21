using Nextflow.Domain.Dtos;
using Nextflow.Domain.Exceptions;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;
using Nextflow.Application.UseCases.Base;

namespace Nextflow.Application.UseCases.StockMovements;

public class CreateStockMovementUseCase(
    IStockMovementRepository repository,
    IProductRepository productRepository
    )
: CreateUseCaseBase<StockMovement, IStockMovementRepository, CreateStockMovementDto, StockMovementResponseDto>(repository)
{
    private readonly IProductRepository _productRepository = productRepository;

    protected override StockMovement MapToEntity(CreateStockMovementDto dto) => new(dto);
    protected override StockMovementResponseDto MapToResponseDto(StockMovement entity) => new(entity);

    protected override async Task BeforePersistence(StockMovement entity, CreateStockMovementDto dto, CancellationToken ct)
    {
        var product = await _productRepository.GetByIdAsync(dto.ProductId, ct) ?? throw new NotFoundException("Produto não encontrado.");
        product.SetMovementStock(dto);
        await _productRepository.UpdateAsync(product, ct);
    }
}