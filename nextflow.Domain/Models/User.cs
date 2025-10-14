using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using nextflow.Domain.Dtos;
using nextflow.Domain.Enums;
using nextflow.Domain.Interfaces.Models;
using nextflow.Domain.Models.Base;

namespace nextflow.Domain.Models;

[Table("Users")]
public class User : Person, IUpdatable<UpdateUserDto>
{
    [Required(ErrorMessage = "A Senha é obrigatória.")]
    public string Password { get; private set; } = string.Empty;

    [Required]
    public RoleEnum Role { get; set; } = RoleEnum.User;
    public string? RefreshToken { get; private set; } = string.Empty;
    public virtual ICollection<StockMovement> StockMovements { get; set; } = [];

    public User() : base() { }

    public User(CreateUserDto dto) : base(dto)
    {
        Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
    }
    public void Update(UpdateUserDto dto)
    {
        base.Update(dto);
    }
    public void UpdatePassword(UpdatePasswordDto dto)
    {
        Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        base.Update();
    }
    public void SetRefreshToken(string token)
    {
        RefreshToken = token;
    }
    public void RevokeRefreshToken()
    {
        RefreshToken = null;
    }
}
