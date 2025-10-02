using nextflow.Domain.Dtos.Base;
using System.ComponentModel.DataAnnotations;

namespace Nextflow.Domain.Dtos;

public class CreateSupplierDto : BaseDto
{
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O Nome do forncedor deve ter no máximo 100 caracteres e no mínimo 2 caracteres."), Required(ErrorMessage = "O Nome do forncedor é obrigatório.")]
    public string Name { get;  set; } = string.Empty;

    [Required(ErrorMessage = "O CNPJ do forncedor é obrigatório.")]
    public string CNPJ { get; set; } = string.Empty;
}

public class UpdateSupplierDto : CreateSupplierDto { }
