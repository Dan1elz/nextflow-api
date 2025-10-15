using nextflow.Domain.Dtos;
using nextflow.Domain.Exceptions;
using nextflow.Domain.Interfaces.Repositories;
using nextflow.Domain.Interfaces.UseCases;
using nextflow.Domain.Models;

namespace nextflow.Application.UseCases.StockMovements
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