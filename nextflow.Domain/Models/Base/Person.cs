using System.ComponentModel.DataAnnotations;
using Nextflow.Domain.Dtos.Base;

namespace Nextflow.Domain.Models.Base;

public class Person : BaseModel
{
    [StringLength(25, MinimumLength = 2, ErrorMessage = "O Nome deve ter no máximo 25 caracteres e no mínimo 2 caracteres."), Required(ErrorMessage = "O Nome é obrigatório.")]
    public string Name { get; set; } = string.Empty;

    [StringLength(25, MinimumLength = 2, ErrorMessage = "O Sobrenome deve ter no máximo 25 caracteres e no mínimo 2 caracteres."), Required(ErrorMessage = "O Sobrenome é obrigatório.")]
    public string LastName { get; set; } = string.Empty;

    [StringLength(150, MinimumLength = 5, ErrorMessage = "O Email deve ter no máximo 150 caracteres e no mínimo 5 caracteres."), Required(ErrorMessage = "O Email é obrigatório."), EmailAddress(ErrorMessage = "O Email informado não é válido.")]
    public string Email { get; set; } = string.Empty;

    [StringLength(14, ErrorMessage = "O CPF deve ter no máximo 14 caracteres."), Required(ErrorMessage = "O CPF é obrigatório.")]
    public string CPF { get; set; } = string.Empty;

    [Required(ErrorMessage = "A Data de Nascimento é obrigatória.")]
    public DateOnly BirthDate { get; set; }

    public Person() : base() { }

    protected Person(CreatePersonDto dto) : base()
    {
        Name = dto.Name;
        LastName = dto.LastName;
        Email = dto.Email;
        CPF = dto.CPF;
    }

    public void Update(UpdatePersonDto dto)
    {
        Name = dto.Name;
        LastName = dto.LastName;
        Email = dto.Email;
        CPF = dto.CPF;
        base.Update();
    }
}
