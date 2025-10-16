using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using nextflow.Domain.Dtos;
using nextflow.Domain.Interfaces.Models;
using nextflow.Domain.Models.Base;
namespace nextflow.Domain.Models;

[Table("categories")]
public class Category : BaseModel, IUpdatable<UpdateCategoryDto>
{
    [StringLength(100, ErrorMessage = "A Descrição deve ter no maximo 100 caracteres")]
    [Required(ErrorMessage = "A Descrição é obrigatória")]
    public string Description { get; private set; } = string.Empty;
    public virtual ICollection<CategoryProduct> CategoryProducts { get; set; } = [];

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
