using nextflow.Domain.Dtos.Base;
using System.ComponentModel.DataAnnotations;

namespace Nextflow.Domain.Dtos;

public class CreateClientDto : BaseDto
{
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O Nome do cliente deve ter no máximo 100 caracteres e no mínimo 2 caracteres."), Required(ErrorMessage = "O Nome do cliente é obrigatório.")]
    public string Name { get; set; } = string.Empty;

    [StringLength(100, MinimumLength = 2, ErrorMessage = "O Sobrenome do cliente deve ter no máximo 100 caracteres e no mínimo 2 caracteres."), Required(ErrorMessage = "O Sobrenome do cliente é obrigatório.")]
    public string Lastname { get; set; } = string.Empty;
    [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "O formato do CPF está incorreto (ex: 000.000.000-00)."), Required(ErrorMessage = "O CPF do cliente é obrigatório.")]
    public string CPF { get; set; } = string.Empty;
}

public class UpdateClientDto : CreateClientDto { }
