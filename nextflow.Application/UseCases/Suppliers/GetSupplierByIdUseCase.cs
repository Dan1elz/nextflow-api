using nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.Suppliers;

public class GetSupplierByIdUseCase(ISupplierRepository repository)
    : GetByIdUseCaseBase<Supplier, ISupplierRepository, SupplierResponseDto>(repository)
{
    protected override SupplierResponseDto MapToResponseDto(Supplier entity) => new(entity);
}
