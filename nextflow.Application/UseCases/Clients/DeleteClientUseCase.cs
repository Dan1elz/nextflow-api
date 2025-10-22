using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.Clients;

public class DeleteClientUseCase(IClientRepository repository)
    : DeleteUseCaseBase<Client, IClientRepository>(repository)
{ }
