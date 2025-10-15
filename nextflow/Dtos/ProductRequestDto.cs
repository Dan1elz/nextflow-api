using nextflow.Domain.Dtos;

namespace nextflow.Dtos;

public class ProductRequestDto : UpdateProductDto
{
    public new IFormFile? Image { get; set; }
}
