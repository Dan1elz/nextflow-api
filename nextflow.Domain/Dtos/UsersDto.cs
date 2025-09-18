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

public class LoginDto
{
    [StringLength(150, MinimumLength = 5, ErrorMessage = "O Email deve ter no máximo 150 caracteres e no mínimo 5 caracteres."), Required(ErrorMessage = "O Email é obrigatório."), EmailAddress(ErrorMessage = "O Email informado não é válido.")]
    public string Email { get; set; } = string.Empty;

    [StringLength(32, MinimumLength = 6, ErrorMessage = "A Senha deve ter no máximo 32 caracteres e no mínimo 6 caracteres."), Required(ErrorMessage = "A Senha é obrigatória.")]
    public string Password { get; set; } = string.Empty;
}

public class LoginResponseDto
{
    public string Token { get; set; } = string.Empty;
    public Guid UserId { get; set; } = Guid.Empty;
}
