using nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Models;
using Nextflow.Domain.Interfaces.Repositories;

namespace Nextflow.Application.UseCases.Clients;

public class UpdateClientUseCase(IClientRepository repository)
    : UpdateUseCaseBase<Client, IClientRepository, UpdateClientDto, ClientResponseDto>(repository)
{
    protected override ClientResponseDto MapToResponseDto(Client entity) => new(entity);
}
