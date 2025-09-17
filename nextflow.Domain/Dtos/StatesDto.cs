using Nextflow.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using nextflow.Domain.Dtos.Base;

namespace Nextflow.Domain.Dtos;

public class CreateStateDto : BaseDto
{
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O Nome do estado deve ter no máximo 100 caracteres e no mínimo 2 caracteres."), Required(ErrorMessage = "O Nome do estado é obrigatório.")]
    public string Name { get; set; } = string.Empty;

    [StringLength(2, MinimumLength = 2, ErrorMessage = "O acrônimo do estado deve ter no máximo 2 caracteres e no mínimo 2 caracteres."), Required(ErrorMessage = "O acrônimo do estado é obrigatório.")]
    public string Acronym { get; set; } = string.Empty;

    [StringLength(2, MinimumLength = 2, ErrorMessage = "O código IBGE deve ter no máximo 2 caracteres e no mínimo 2 caracteres."), Required(ErrorMessage = "O código IBGE é obrigatório.")]
    public string IbgeCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Id do país é obrigatório.")]
    public Guid CountryId { get; set; }
}

public class UpdateStateDto : CreateStateDto { }
