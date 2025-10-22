

using nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.Clients;

public class GetAllClientUseCase(IClientRepository repository)
    : GetAllUseCaseBase<Client, IClientRepository, ClientResponseDto>(repository)
{
    protected override ClientResponseDto MapToResponseDto(Client entity) => new(entity);
}
