using System.ComponentModel.DataAnnotations;
using nextflow.Domain.Attributes;
using nextflow.Domain.Dtos.Base;
using nextflow.Domain.Models;
using nextflow.Domain.Models.Base;

namespace nextflow.Domain.Dtos;

public class CategoriesDto
{

}
public class CreateCategoryDto : BaseDto
{
    [StringLength(100, ErrorMessage = "A Descrição deve ter no maximo 100 caracteres")]
    [Required(ErrorMessage = "A Descrição é obrigatória")]
    public string Description { get; set; } = string.Empty;
}

public class UpdateCategoryDto : CreateCategoryDto { }

public class CategoriyResponseDto : BaseDto
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;

    public CategoriyResponseDto() { }

    public CategoriyResponseDto(Category entity)
    {
        Id = entity.Id;
        Description = entity.Description;
    }
}

public class CreateCategoryProductDto : BaseModel
{
    [NotEmptyGuid(ErrorMessage = "A Categoria é obrigatória.")]
    public Guid CategoryId { get; set; }

    [NotEmptyGuid(ErrorMessage = "O Produto é obrigatório.")]
    public Guid ProductId { get; set; }
}