using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using nextflow.Domain.Dtos;
using nextflow.Domain.Models.Base;

namespace nextflow.Domain.Models;

[Table("Users")]
public class User : Person
{
    [Required(ErrorMessage = "A Senha é obrigatória.")]
    public string Password { get; private set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;

    public User() : base() { }

    public User(CreateUserDto dto) : base(dto)
    {
        Password = dto.Password;
    }
    public void Update(UpdateUserDto dto)
    {
        base.Update(dto);
    }
    public void UpdatePassword(UpdatePasswordDto dto)
    {
        Password = dto.Password;
        base.Update();
    }
}
