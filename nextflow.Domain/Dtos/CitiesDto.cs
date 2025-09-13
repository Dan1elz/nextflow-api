using Nextflow.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using nextflow.Domain.Dtos.Base;

namespace Nextflow.Domain.Dtos;

public class CreateCityDto : BaseDto
{
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O Nome da cidade deve ter no máximo 100 caracteres e no mínimo 2 caracteres."), Required(ErrorMessage = "O Nome da cidade é obrigatório.")]
    public string Name { get; private set; } = string.Empty;

    [StringLength(2, MinimumLength = 2, ErrorMessage = "O código IBGE deve ter no máximo 2 caracteres e no mínimo 2 caracteres."), Required(ErrorMessage = "O código IBGE é obrigatório.")]
    public string IbgeCode { get; private set; } = string.Empty;

    [ForeignKey("states"), Required(ErrorMessage = "Id do estado é obrigatório.")]
    public Guid StateId { get; private set; }
    public virtual State? State { get; set; }
}

public class UpdateCityDto : CreateCityDto { }
