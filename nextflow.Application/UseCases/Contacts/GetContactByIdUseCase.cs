using nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Models;
using Nextflow.Domain.Interfaces.Repositories;

namespace Nextflow.Application.UseCases.Contacts;

internal class GetContactByIdUseCase(IContactRepository repository)
: GetByIdUseCaseBase<Contact, IContactRepository, ContactResponseDto>(repository)
{
    protected override ContactResponseDto MapToResponseDto(Contact entity) => new(entity);
}
