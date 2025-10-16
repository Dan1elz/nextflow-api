using nextflow.Domain.Dtos.Base;
using System.ComponentModel.DataAnnotations;
using nextflow.Domain.Attributes;

namespace Nextflow.Domain.Dtos;

public class CreateContactDto : BaseDto
{
    [NotEmptyGuid(ErrorMessage = "Id do cliente é obrigatório.")]
    public Guid ClientId { get; set; }

    [NotEmptyGuid(ErrorMessage = "Id do fornecedor é obrigatório.")]
    public Guid SupplierId { get; set; }

    [StringLength(100, MinimumLength = 2, ErrorMessage = "A Descrição do contato deve ter no máximo 100 caracteres e no mínimo 2 caracteres."), Required(ErrorMessage = "A descrição do contato é obrigatório.")]
    public string Description { get; set; } = string.Empty;

    [StringLength(15, ErrorMessage = "O telefone deve ter no máximo 15 caracteres."), Required(ErrorMessage = "O telefone é obrigatório.")]
    public string Fone { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
    public string Email { get; set; } = string.Empty;
}

public class UpdateContactDto : CreateContactDto { }

public class ContactResponseDto : BaseDto
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Guid SupplierId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Fone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public ContactResponseDto() { }

    public ContactResponseDto(Contact entity)
    {
        Id = entity.Id;
        ClientId = entity.ClientId;
        SupplierId = entity.SupplierId;
        Description = entity.Description;
        Fone = entity.Fone;
        Email = entity.Email;
    }
}
