using nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Models;
using Nextflow.Domain.Interfaces.Repositories;

namespace Nextflow.Application.UseCases.Suppliers;

internal class CreateSupplierUseCase(ISupplierRepository repository)
: CreateUseCaseBase<Supplier, ISupplierRepository, CreateSupplierDto, SupplierResponseDto>(repository)
{
    protected override Supplier MapToEntity(CreateSupplierDto dto) => new(dto);
    protected override SupplierResponseDto MapToResponseDto(Supplier entity) => new(entity);
}