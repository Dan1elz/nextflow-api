using nextflow.Domain.Models.Base;
using Nextflow.Domain.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nextflow.Domain.Models;

[Table("suppliers")]
public class Supplier : BaseModel
{
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O Nome do forncedor deve ter no máximo 100 caracteres e no mínimo 2 caracteres."), Required(ErrorMessage = "O Nome do forncedor é obrigatório.")]
    public string Name { get; private set; } = string.Empty;

    [Required(ErrorMessage = "O CNPJ do forncedor é obrigatório.")]
    public string CNPJ { get; private set; } = string.Empty;

    private Supplier() : base() { }

    public Supplier(CreateSupplierDto dto) : base()
    {
        Name = dto.Name;
        CNPJ = dto.CNPJ;
    }

    public void Update(UpdateSupplierDto dto)
    {
        Name = dto.Name;
        CNPJ = dto.CNPJ;
    }
}
