using nextflow.Domain.Dtos.Base;
using Nextflow.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Nextflow.Domain.Dtos;

public class CreateAddressDto : BaseDto
{
    [ForeignKey("clients"), Required(ErrorMessage = "Id do cliente é obrigatório.")]
    public Guid ClientId { get; set; }
    public virtual Client? Client { get; set; }

    [ForeignKey("suppliers"), Required(ErrorMessage = "Id do fornecedor é obrigatório.")]
    public Guid SupplierId { get; set; }
    public virtual Supplier? Supplier { get; set; }

    [StringLength(100, MinimumLength = 2, ErrorMessage = "A Descrição do endereço deve ter no máximo 100 caracteres e no mínimo 2 caracteres."), Required(ErrorMessage = "A descrição do endereço é obrigatório.")]
    public string Description { get; set; } = string.Empty;

    [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome da rua deve ter no máximo 100 caracteres e no mínimo 2 caracteres."), Required(ErrorMessage = "O nome da rua é obrigatório.")]
    public string Street { get; set; } = string.Empty;

    [StringLength(10, MinimumLength = 1, ErrorMessage = "O número deve ter no máximo 10 caracteres e no mínimo 1 caracteres."), Required(ErrorMessage = "O número é obrigatório.")]
    public string Number { get; set; } = string.Empty;

    [ForeignKey("cities"), Required(ErrorMessage = "Id da cidade é obrigatório.")]
    public Guid CityId { get; set; }
    public virtual City? City { get; set; }

    [ForeignKey("States"), Required(ErrorMessage = "Id do estado é obrigatório.")]
    public Guid StateId { get; set; }
    public virtual State? State { get; set; }

    [StringLength(100, MinimumLength = 0, ErrorMessage = "O complemento deve ter no máximo 100 cqracteres.")]
    public string Complement { get; set; } = string.Empty;

    [StringLength(9, MinimumLength = 9, ErrorMessage = "O CEP deve ter 9 caracteres."), Required(ErrorMessage = "O CEP é obrigatório.")]
    [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "Formato de CEP inválido (ex: 00000-000).")]]
    public string ZipCode { get; set; } = string.Empty;
}

public class UpdateAddressDto : CreateAddressDto { }