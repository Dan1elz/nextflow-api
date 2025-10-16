using nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Models;
using Nextflow.Domain.Interfaces.Repositories;

namespace Nextflow.Application.UseCases.Addresses;

public class GetAddressByIdUseCase(IAddressRepository repository)
    : GetByIdUseCaseBase<Address, IAddressRepository, AddressResponseDto>(repository)
{
    protected override AddressResponseDto MapToResponseDto(Address entity) => new(entity);
}

