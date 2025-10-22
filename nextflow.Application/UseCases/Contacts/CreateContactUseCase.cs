using nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Models;
using Nextflow.Domain.Interfaces.Repositories;

namespace Nextflow.Application.UseCases.Contacts;

public class CreateContactUseCase(IContactRepository repository)
    : CreateUseCaseBase<Contact, IContactRepository, CreateContactDto, ContactResponseDto>(repository)
{
    protected override Contact MapToEntity(CreateContactDto dto) => new(dto);
    protected override ContactResponseDto MapToResponseDto(Contact entity) => new(entity);
}
