using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.Contacts;

public class DeleteClientUseCase(IContactRepository repository)
    : DeleteUseCaseBase<Contact, IContactRepository>(repository)
{ }

