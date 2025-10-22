using Nextflow.Domain.Dtos;
using Nextflow.Domain.Exceptions;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Interfaces.UseCases;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.StockMovements
{
    public class CreateStockMovementUseCase(
        IStockMovementRepository repository, IProductRepository productRepository
        ) : ICreateStockMovementUseCase
    {
        private readonly IStockMovementRepository _repository = repository;
        private readonly IProductRepository _productRepository = productRepository;

        public async Task<StockMovementResponseDto> Execute(CreateStockMovementDto dto, CancellationToken ct)
        {
            dto.Validate();
            StockMovement stockMovement = new(dto);

            var product = await _productRepository.GetByIdAsync(dto.ProductId, ct) ?? throw new NotFoundException("Produto não encontrado."); //tem que trocar para o usecase do produto
            product.SetMovementStock(dto);

            await _productRepository.UpdateAsync(product, ct);

            await _repository.AddAsync(stockMovement, ct);
            return new StockMovementResponseDto(stockMovement);
        }
    }
}