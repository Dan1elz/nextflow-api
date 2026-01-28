using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;
using Nextflow.Domain.Exceptions;

namespace Nextflow.Application.UseCases.Clients;

public class CreateClientUseCase(IClientRepository repository)
    : CreateUseCaseBase<Client, IClientRepository, CreateClientDto, ClientResponseDto>(repository)
{
    protected override Client MapToEntity(CreateClientDto dto) => new(dto);
    protected override ClientResponseDto MapToResponseDto(Client entity) => new(entity);

    protected override async Task ValidateBusinessRules(CreateClientDto dto, CancellationToken ct) {
        var existingClient = await _repository.ExistsAsync(c => c.Email == dto.Email || c.CPF == dto.CPF, ct);
        
        if (existingClient) {
            throw new BadRequestException("Email ou CPF já estão em uso");  
        }
    }
}

