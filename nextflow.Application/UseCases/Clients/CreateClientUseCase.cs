using nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.Clients;

public class CreateClientUseCase(IClientRepository repository)
    : CreateUseCaseBase<Client, IClientRepository, CreateClientDto, ClientResponseDto>(repository)
{
    protected override Client MapToEntity(CreateClientDto dto) => new(dto);
    protected override ClientResponseDto MapToResponseDto(Client entity) => new(entity);
}

