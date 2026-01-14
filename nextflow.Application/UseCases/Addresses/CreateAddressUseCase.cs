using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Exceptions;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;
using Nextflow.Infrastructure.Repositories;


namespace Nextflow.Application.UseCases.Addresses;

public class CreateAddressUseCase(IAddressRepository repository, IClientRepository clientRepository, ISupplierRepository supplierRepository)
    : CreateUseCaseBase<Address, IAddressRepository, CreateAddressDto, AddressResponseDto>(repository)
{
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly ISupplierRepository _supplierRepository = supplierRepository;
    protected override Address MapToEntity(CreateAddressDto dto) => new(dto);
    protected override AddressResponseDto MapToResponseDto(Address entity) => new(entity);
    protected override async Task ValidateBusinessRules(CreateAddressDto dto, CancellationToken ct)
    {
        if (dto.ClientId.HasValue)
        {
            var clientExistsAndIsActive = await _clientRepository.ExistsAsync(
                c => c.Id == dto.ClientId.Value && c.IsActive,
                ct
            );

            if (!clientExistsAndIsActive) throw new NotFoundException($"O Cliente informado não existe ou está inativo.");
        }

        if (dto.SupplierId.HasValue)
        {
            var supplierExistsAndIsActive = await _supplierRepository.ExistsAsync(
                s => s.Id == dto.SupplierId.Value && s.IsActive,
                ct
            );

            if (!supplierExistsAndIsActive) throw new NotFoundException($"O Fornecedor informado não existe ou está inativo.");
        }
    }
}


