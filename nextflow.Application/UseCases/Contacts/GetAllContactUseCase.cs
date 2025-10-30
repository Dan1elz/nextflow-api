using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Models;
using Nextflow.Domain.Interfaces.Repositories;

namespace Nextflow.Application.UseCases.Contacts;

public class GetAllContactUseCase(IContactRepository repository)
    : GetAllUseCaseBase<Contact, IContactRepository, ContactResponseDto>(repository)
{
    protected override ContactResponseDto MapToResponseDto(Contact entity) => new(entity);
}

