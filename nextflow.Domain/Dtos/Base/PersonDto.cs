using System.ComponentModel.DataAnnotations;
using Nextflow.Domain.Attributes;

namespace Nextflow.Domain.Dtos.Base;

public class CreatePersonDto : BaseDto
{
    [StringLength(25, MinimumLength = 2, ErrorMessage = "O Nome deve ter no máximo 25 caracteres e no mínimo 2 caracteres."), Required(ErrorMessage = "O Nome é obrigatório.")]
    public string Name { get; set; } = string.Empty;

    [StringLength(25, MinimumLength = 2, ErrorMessage = "O Sobrenome deve ter no máximo 25 caracteres e no mínimo 2 caracteres."), Required(ErrorMessage = "O Sobrenome é obrigatório.")]
    public string LastName { get; set; } = string.Empty;

    [CpfCnpj, Required(ErrorMessage = "O CPF é obrigatório.")]
    public string CPF { get; set; } = string.Empty;

    [Required(ErrorMessage = "A Data de Nascimento é obrigatória.")]
    [AnoValido(1900, -1)]
    public DateOnly BirthDate { get; set; }
}

public class UpdatePersonDto : CreatePersonDto
{ }


public class PersonResponseDto
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; } = DateOnly.MinValue;
    public bool IsActive { get; set; } = false;
}