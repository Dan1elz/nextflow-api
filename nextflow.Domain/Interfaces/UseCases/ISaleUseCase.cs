using Nextflow.Domain.Dtos;

namespace nextflow.Domain.Interfaces.UseCases;

public interface ICreateSaleUseCase
{
    Task<SaleResponseDto> Execute(CreateSaleDto dto, CancellationToken ct);
}
public interface IDeleteSaleUseCase
{
    Task Execute(Guid id, Guid userId, CancellationToken ct);
}