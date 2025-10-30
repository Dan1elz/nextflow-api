using Microsoft.EntityFrameworkCore;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Exceptions;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Interfaces.UseCases.Base;

namespace nextflow.Application.UseCases.Sales;

public class GetSaleByIdUseCase(ISaleRepository repository) : IGetByIdUseCase<SaleResponseDto>
{
    public async Task<SaleResponseDto> Execute(Guid id, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(id, ct, x => x.Include(s => s.Payments))
            ?? throw new NotFoundException($"Venda não encontrada com o Id: {id}");

        return new SaleResponseDto(entity);
    }
}