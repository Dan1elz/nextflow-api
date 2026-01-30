using Nextflow.Domain.Dtos.Base;
using Nextflow.Domain.Models;

namespace Nextflow.Domain.Dtos;

public class CreateClientDto : CreatePersonDto { }

public class UpdateClientDto : UpdatePersonDto { }

public class ClientResponseDto : PersonResponseDto
{
    public ClientResponseDto() { }

    public ClientResponseDto(Client entity)
    {
        Id = entity.Id;
        Name = entity.Name;
        LastName = entity.LastName;
        CPF = entity.CPF;
        BirthDate = entity.BirthDate;
        IsActive = entity.IsActive;
    }
}