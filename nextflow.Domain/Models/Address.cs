using nextflow.Domain.Models.Base;
using Nextflow.Domain.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace Nextflow.Domain.Models;

[Table("addresses")]
public class Address : BaseModel
{
    [ForeignKey("clients"), Required(ErrorMessage = "Id do cliente é obrigatório.")]
    public Guid ClientId { get; private set; }
    public virtual Client? Client { get; private set; }

    [ForeignKey("suppliers"), Required(ErrorMessage = "Id do fornecedor é obrigatório.")]
    public Guid SupplierId { get; private set; }
    public virtual Supplier? Supplier { get; private set; }

    [StringLength(100, MinimumLength = 2, ErrorMessage = "A Descrição do endereço deve ter no máximo 100 caracteres e no mínimo 2 caracteres."), Required(ErrorMessage = "A descrição do endereço é obrigatório.")]
    public string Description { get; private set; } = string.Empty;

    [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome da rua deve ter no máximo 100 caracteres e no mínimo 2 caracteres."), Required(ErrorMessage = "O nome da rua é obrigatório.")]
    public string Street { get; private set; } = string.Empty;

    [StringLength(10, MinimumLength = 1, ErrorMessage = "O número deve ter no máximo 10 caracteres e no mínimo 1 caracteres."), Required(ErrorMessage = "O número é obrigatório.")]
    public string Number { get; private set; } = string.Empty;

    [ForeignKey("cities"), Required(ErrorMessage = "Id da cidade é obrigatório.")]
    public Guid CityId { get; private set; }
    public virtual City? City { get; private set; }

    [ForeignKey("States"), Required(ErrorMessage = "Id do estado é obrigatório.")]
    public Guid StateId { get; private set; }
    public virtual State? State { get; private set; }

    [StringLength(100, MinimumLength = 0, ErrorMessage = "O complemento deve ter no máximo 100 cqracteres.")]
    public string Complement { get; private set; } = string.Empty;

    [StringLength(9, MinimumLength = 9, ErrorMessage = "O CEP deve ter 9 caracteres."), Required(ErrorMessage = "O CEP é obrigatório.")]
    [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "Formato de CEP inválido (ex: 00000-000).")]]
    public string ZipCode { get; private set; } = string.Empty;


    private Address() : base() { }


    public Address(CreateAddressDto dto) : base()
    {
        ClientId = dto.ClientId;
        SupplierId = dto.SupplierId;
        Description = dto.Description;
        Street = dto.Street;
        Number = dto.Number;
        CityId = dto.CityId;
        StateId = dto.StateId;
        Complement = dto.Complement;
        ZipCode = dto.ZipCode;
    }

    public void Update(UpdateAddressDto dto) 
    {
        ClientId = dto.ClientId;
        SupplierId = dto.SupplierId;
        Description = dto.Description;
        Street = dto.Street;
        Number = dto.Number;
        CityId = dto.CityId;
        StateId = dto.StateId;
        Complement = dto.Complement;
        ZipCode = dto.ZipCode;
    }



    
}
