using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nextflow.Domain.Interfaces.Models;
using Nextflow.Domain.Models.Base;
using Nextflow.Domain.Dtos;

namespace Nextflow.Domain.Models;

[Table("countries")]
public class Country : BaseModel, IUpdatable<UpdateCountryDto>
{
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O Nome do país deve ter no máximo 100 caracteres e no mínimo 2 caracteres."), Required(ErrorMessage = "O Nome do país é obrigatório.")]
    public string Name { get; private set; } = string.Empty;

    [StringLength(2, MinimumLength = 2, ErrorMessage = "O acrônimo do país deve ter no máximo 2 caracteres e no mínimo 2 caracteres."), Required(ErrorMessage = "O acrônimo do país é obrigatório.")]
    public string AcronymIso { get; private set; } = string.Empty;

    [StringLength(255, MinimumLength = 2, ErrorMessage = "O código Sefaz deve ter no máximo 255 caracteres e no mínimo 2 caracteres.")]
    public string? SefazCode { get; private set; } = string.Empty;

    [StringLength(255, MinimumLength = 2, ErrorMessage = "O código do Bacen deve ter no máximo 255 caracteres e no mínimo 2 caracteres.")]
    public string? BacenCode { get; private set; } = string.Empty;
    public virtual ICollection<State> States { get; set; } = [];

    private Country() : base() { }

    public Country(CreateCountryDto dto) : base()
    {
        Name = dto.Name;
        AcronymIso = dto.AcronymIso;
        SefazCode = dto.SefazCode;
        BacenCode = dto.BacenCode;
    }

    public void Update(UpdateCountryDto dto)
    {
        Name = dto.Name;
        AcronymIso = dto.AcronymIso;
        SefazCode = dto.SefazCode;
        BacenCode = dto.BacenCode;
        base.Update();
    }

}
