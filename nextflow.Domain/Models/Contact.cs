﻿using nextflow.Domain.Interfaces.Models;
using nextflow.Domain.Models.Base;
using Nextflow.Domain.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nextflow.Domain.Models;

[Table("contacts")]
public class Contact : BaseModel, IUpdatable<UpdateContactDto>
{
    [ForeignKey("clients")]
    public Guid? ClientId { get; private set; }
    public virtual Client? Client { get; private set; }

    [ForeignKey("suppliers")]
    public Guid? SupplierId { get; private set; }
    public virtual Supplier? Supplier { get; private set; }

    [StringLength(100, MinimumLength = 2, ErrorMessage = "A Descrição do contato deve ter no máximo 100 caracteres e no mínimo 2 caracteres."), Required(ErrorMessage = "A descrição do contato é obrigatório.")]
    public string Description { get; private set; } = string.Empty;

    [StringLength(15, ErrorMessage = "O telefone deve ter no máximo 15 caracteres."), Required(ErrorMessage = "O telefone é obrigatório.")]
    public string Fone { get; private set; } = string.Empty;

    [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
    public string Email { get; private set; } = string.Empty;

    private Contact() : base() { }

    public Contact(CreateContactDto dto) : base()
    {
        ClientId = dto.ClientId;
        SupplierId = dto.SupplierId;
        Description = dto.Description;
        Fone = dto.Fone;
        Email = dto.Email;
    }

    public void Update(UpdateContactDto dto)
    {
        Description = dto.Description;
        Fone = dto.Fone;
        Email = dto.Email;
    }
}
