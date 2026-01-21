using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Models;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Exceptions;

namespace Nextflow.Application.UseCases.Contacts;

public class CreateContactUseCase(IContactRepository repository, IClientRepository clientRepository, ISupplierRepository supplierRepository)
    : CreateUseCaseBase<Contact, IContactRepository, CreateContactDto, ContactResponseDto>(repository)
{
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly ISupplierRepository _supplierRepository = supplierRepository;
    protected override Contact MapToEntity(CreateContactDto dto) => new(dto);
    protected override ContactResponseDto MapToResponseDto(Contact entity) => new(entity);

    protected override async Task ValidateBusinessRules(CreateContactDto dto, CancellationToken ct)
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
