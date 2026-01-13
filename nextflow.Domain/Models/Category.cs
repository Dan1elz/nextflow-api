using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Models;
using Nextflow.Domain.Models.Base;
namespace Nextflow.Domain.Models;

[Table("categories")]
public class Category : BaseModel, IUpdatable<UpdateCategoryDto>
{
    [StringLength(100, ErrorMessage = "A Descrição deve ter no maximo 100 caracteres")]
    [Required(ErrorMessage = "A Descrição é obrigatória")]
    public string Description { get; private set; } = string.Empty;
    public virtual ICollection<CategoryProduct> CategoryProducts { get; set; } = [];

    public override string Preposition => "a";
    public override string Singular => "categoria";
    public override string Plural => "categorias";

    private Category() : base() { }

    public Category(CreateCategoryDto dto) : base()
    {
        Description = dto.Description;
    }

    public void Update(UpdateCategoryDto dto)
    {
        Description = dto.Description;
        base.Update();
    }
}
