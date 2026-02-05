using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.Suppliers;

public class GetAllSuppliersUseCase(ISupplierRepository repository)
    : GetAllUseCaseBase<Supplier, ISupplierRepository, SupplierResponseDto>(repository)
{
    protected override SupplierResponseDto MapToResponseDto(Supplier entity) => new(entity);
}
