using nextflow.Domain.Interfaces.Models;
using nextflow.Domain.Models.Base;
using Nextflow.Domain.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nextflow.Domain.Models;

[Table("cities")]
public class City : BaseModel, IUpdatable<UpdateCityDto>
{
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O Nome da cidade deve ter no máximo 100 caracteres e no mínimo 2 caracteres."), Required(ErrorMessage = "O Nome da cidade é obrigatório.")]
    public string Name { get; private set; } = string.Empty;

    [StringLength(2, MinimumLength = 2, ErrorMessage = "O código IBGE deve ter no máximo 2 caracteres e no mínimo 2 caracteres."), Required(ErrorMessage = "O código IBGE é obrigatório.")]
    public string IbgeCode { get; private set; } = string.Empty;

    [ForeignKey("states"), Required(ErrorMessage = "Id do estado é obrigatório.")]
    public Guid StateId { get; private set; }
    public virtual State? State { get; set; }
    public virtual ICollection<Address> Addresses { get; set; } = [];

    private City() : base() { }


    public City(CreateCityDto dto) : base()
    {
        Name = dto.Name;
        IbgeCode = dto.IbgeCode;
        StateId = dto.StateId;
    }

    public void Update(UpdateCityDto dto)
    {
        Name = dto.Name;
        IbgeCode = dto.IbgeCode;
        StateId = dto.StateId;
        base.Update();
    }
}
