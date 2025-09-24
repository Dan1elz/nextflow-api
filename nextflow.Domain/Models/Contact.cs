using nextflow.Domain.Models.Base;
using Nextflow.Domain.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nextflow.Domain.Models;

[Table("contacts")]
public class Contact : BaseModel
{
    [ForeignKey("clients"), Required(ErrorMessage = "Id do cliente é obrigatório.")]
    public Guid ClientId { get; private set; }
    public virtual Client? Client { get; private set; }

    [ForeignKey("suppliers"), Required(ErrorMessage = "Id do fornecedor é obrigatório.")]
    public Guid SupplierId { get; private set; }
    public virtual Supplier? Supplier { get; private set; }

    [StringLength(100, MinimumLength = 2, ErrorMessage = "A Descrição do contato deve ter no máximo 100 caracteres e no mínimo 2 caracteres."), Required(ErrorMessage = "A descrição do contato é obrigatório.")]
    public string Description { get; private set; } = string.Empty;

    [StringLength(15, ErrorMessage = "O telefone deve ter no máximo 15 caracteres."), Required(ErrorMessage = "O telefone é obrigatório."),
    RegularExpression(@"^\(?(?:[14689][1-9]|2[12478]|3[1234578]|5[1345]|7[134579])\)? ?(?:[2-8]|9[1-9])[0-9]{3}\-?[0-9]{4}$", ErrorMessage = "Formato de telefone inválido.")]
    public string Fone { get; private set; } = string.Empty;

    [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
    public string Email { get; private set; } = string.Empty;

    private Contact() : base() { }

    public Contact(CreateContactDto dto) : base()
    {
        Client = dto.Client;
        SupplierId = dto.SupplierId;
        Description = dto.Description;
        Fone = dto.Fone;
        Email = dto.Email;
    }

    public void Update(UpdateContactDto dto)
    {
        Client = dto.Client;
        SupplierId = dto.SupplierId;
        Description = dto.Description;
        Fone = dto.Fone;
        Email = dto.Email;
    }
}
