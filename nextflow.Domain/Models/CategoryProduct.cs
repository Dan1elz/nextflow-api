
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using nextflow.Domain.Dtos;
using nextflow.Domain.Models.Base;

namespace nextflow.Domain.Models;

[Table("category_products")]
public class CategoryProduct : BaseModel
{
    [ForeignKey("categories"), Required(ErrorMessage = "A Categoria é obrigatória.")]
    public Guid CategoryId { get; private set; }
    public virtual Category? Category { get; set; }

    [ForeignKey("products"), Required(ErrorMessage = "O Produto é obrigatório.")]
    public Guid ProductId { get; private set; }
    public virtual Product? Product { get; set; }

    private CategoryProduct() : base() { }

    public CategoryProduct(CreateCategoryProductDto dto) : base()
    {
        CategoryId = dto.CategoryId;
        ProductId = dto.ProductId;
    }
}
