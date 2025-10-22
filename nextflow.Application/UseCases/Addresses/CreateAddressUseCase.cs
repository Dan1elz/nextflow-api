using nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Models;
using Nextflow.Domain.Interfaces.Repositories;


namespace Nextflow.Application.UseCases.Addresses;

public class CreateAddressUseCase(IAddressRepository repository)
    : CreateUseCaseBase<Address, IAddressRepository, CreateAddressDto, AddressResponseDto>(repository)
{
    protected override Address MapToEntity(CreateAddressDto dto) => new(dto);
    protected override AddressResponseDto MapToResponseDto(Address entity) => new(entity);
}

