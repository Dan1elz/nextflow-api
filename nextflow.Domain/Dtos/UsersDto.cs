using System.ComponentModel.DataAnnotations;
using nextflow.Domain.Dtos.Base;

namespace nextflow.Domain.Dtos;

public class CreateUserDto : CreatePersonDto
{
    [StringLength(32, MinimumLength = 6, ErrorMessage = "A Senha deve ter no máximo 32 caracteres e no mínimo 6 caracteres."), Required(ErrorMessage = "A Senha é obrigatória.")]
    public string Password { get; set; } = string.Empty;
}

public class UpdateUserDto : UpdatePersonDto
{ }

public class UpdatePasswordDto
{
    [StringLength(32, MinimumLength = 6, ErrorMessage = "A Senha deve ter no máximo 32 caracteres e no mínimo 6 caracteres."), Required(ErrorMessage = "A Senha é obrigatória.")]
    public string Password { get; set; } = string.Empty;
}
