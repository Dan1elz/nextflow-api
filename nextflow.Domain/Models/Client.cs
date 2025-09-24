using nextflow.Domain.Models.Base;
using Nextflow.Domain.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nextflow.Domain.Models;

[Table("clients")]
public class Client : BaseModel
{
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O Nome do cliente deve ter no máximo 100 caracteres e no mínimo 2 caracteres."), Required(ErrorMessage = "O Nome do cliente é obrigatório.")]
    public string Name { get; private set; } = string.Empty;

    [StringLength(100, MinimumLength = 2, ErrorMessage = "O Sobrenome do cliente deve ter no máximo 100 caracteres e no mínimo 2 caracteres."), Required(ErrorMessage = "O Sobrenome do cliente é obrigatório.")]
    public string Lastname { get; private set; } = string.Empty;
    [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "O formato do CPF está incorreto (ex: 000.000.000-00)."), Required(ErrorMessage = "O CPF do cliente é obrigatório.")]
    public string CPF { get; private set; } = string.Empty;


    private Client() : base() { }

    public Client(CreateClientDto dto) : base()
    {
        Name = dto.Name;
        Lastname = dto.Lastname;
        CPF = dto.CPF;
    }

    public void Update(UpdateClientDto dto)
    {
        Name = dto.Name;
        Lastname = dto.Lastname;
        CPF = dto.CPF;
    }
}
