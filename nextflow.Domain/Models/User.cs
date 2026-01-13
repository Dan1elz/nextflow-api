using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Enums;
using Nextflow.Domain.Interfaces.Models;
using Nextflow.Domain.Models.Base;

namespace Nextflow.Domain.Models;

[Table("users")]
public class User : Person, IUpdatable<UpdateUserDto>
{
    [Required(ErrorMessage = "A Senha é obrigatória.")]
    public string Password { get; private set; } = string.Empty;

    [Required]
    public RoleEnum Role { get; set; } = RoleEnum.User;
    public string? RefreshToken { get; private set; } = string.Empty;
    public virtual ICollection<StockMovement> StockMovements { get; set; } = [];
    public virtual ICollection<Sale> Sales { get; set; } = [];

    public override string Preposition => "o";
    public override string Singular => "usuário";
    public override string Plural => "usuários";

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
