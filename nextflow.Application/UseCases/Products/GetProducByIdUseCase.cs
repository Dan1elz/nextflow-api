using Microsoft.EntityFrameworkCore;
using nextflow.Domain.Dtos;
using nextflow.Domain.Exceptions;
using nextflow.Domain.Interfaces.Repositories;
using nextflow.Domain.Interfaces.UseCases.Base;

namespace nextflow.Application.UseCases.Products;

public class GetProductByIdUseCase(IProductRepository repository)
    : IGetByIdUseCase<ProductResponseDto>
{
    private readonly IProductRepository _repository = repository;
    public async Task<ProductResponseDto> Execute(Guid id, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(id, ct, x => x.Include(c => c.CategoryProducts).ThenInclude(cp => cp.Category))
            ?? throw new NotFoundException($"Produto não encontrado com o Id: {id}");

        return new ProductResponseDto(entity);
    }
}