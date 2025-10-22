using Microsoft.EntityFrameworkCore;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Exceptions;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Interfaces.UseCases.Base;

namespace Nextflow.Application.UseCases.Products;

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