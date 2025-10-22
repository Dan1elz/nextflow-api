using nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Models;
using Nextflow.Domain.Interfaces.Repositories;

namespace Nextflow.Application.UseCases.Contacts;

internal class UpdateContactUseCase(IContactRepository repository)
    : UpdateUseCaseBase<Contact, IContactRepository, UpdateContactDto, ContactResponseDto>(repository)
{
    protected override ContactResponseDto MapToResponseDto(Contact entity) => new(entity);
}
