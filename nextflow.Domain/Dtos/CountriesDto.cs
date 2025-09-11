using nextflow.Domain.Dtos.Base;
using System.ComponentModel.DataAnnotations;

namespace Nextflow.Domain.Dtos;

public class CreateCountryDto : BaseDto
{
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O Nome do país deve ter no máximo 100 caracteres e no mínimo 2 caracteres."), Required(ErrorMessage = "O Nome do país é obrigatório.")]
    public string Name { get; set; } = string.Empty;

    [StringLength(2, MinimumLength = 2, ErrorMessage = "O acrônimo do país deve ter no máximo 2 caracteres e no mínimo 2 caracteres."), Required(ErrorMessage = "O acrônimo do país é obrigatório.")]
    public string AcronymIso { get; set; } = string.Empty;

    [StringLength(255, MinimumLength = 2, ErrorMessage = "O código Sefaz deve ter no máximo 255 caracteres e no mínimo 2 caracteres."), Required(ErrorMessage = "O código Sefaz é obrigatório.")]
    public string SefazCode { get; set; } = string.Empty;

    [StringLength(255, MinimumLength = 2, ErrorMessage = "O código do Bacen deve ter no máximo 255 caracteres e no mínimo 2 caracteres.")]
    public string BacenCode { get; set; } = string.Empty;
}

public class UpdateCountryDto : CreateCountryDto { }






