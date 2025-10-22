using Nextflow.Domain.Dtos;

namespace Nextflow.Dtos;

public class ProductRequestDto : UpdateProductDto
{
    public new IFormFile? Image { get; set; }
}
