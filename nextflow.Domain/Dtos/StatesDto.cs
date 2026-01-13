using System.ComponentModel.DataAnnotations;
using Nextflow.Domain.Attributes;
using Nextflow.Domain.Dtos.Base;
using Nextflow.Domain.Models;

namespace Nextflow.Domain.Dtos;

public class CreateStateDto : BaseDto
{
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O Nome do estado deve ter no máximo 100 caracteres e no mínimo 2 caracteres."), Required(ErrorMessage = "O Nome do estado é obrigatório.")]
    public string Name { get; set; } = string.Empty;

    [StringLength(2, MinimumLength = 2, ErrorMessage = "O acrônimo do estado deve ter no máximo 2 caracteres e no mínimo 2 caracteres."), Required(ErrorMessage = "O acrônimo do estado é obrigatório.")]
    public string Acronym { get; set; } = string.Empty;

    [StringLength(2, MinimumLength = 2, ErrorMessage = "O código IBGE deve ter no máximo 2 caracteres e no mínimo 2 caracteres."), Required(ErrorMessage = "O código IBGE é obrigatório.")]
    public string IbgeCode { get; set; } = string.Empty;

    [NotEmptyGuid(ErrorMessage = "Id do país é obrigatório.")]
    public Guid CountryId { get; set; }
}

public class UpdateStateDto : CreateStateDto { }

public class StateResponseDto : BaseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Acronym { get; set; } = string.Empty;
    public string IbgeCode { get; set; } = string.Empty;
    public Guid CountryId { get; set; }
    public CountryResponseDto? Country { get; set; }

    public StateResponseDto() { }

    public StateResponseDto(State entity)
    {
        Id = entity.Id;
        Name = entity.Name;
        Acronym = entity.Acronym;
        IbgeCode = entity.IbgeCode;
        CountryId = entity.CountryId;
        Country = entity.Country != null ? new CountryResponseDto(entity.Country) : null;
    }
}
