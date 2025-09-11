using nextflow.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nextflow.Domain.Models;

[Table("states")]
public class State : BaseModel
{
    public string Name { get; private set; } = string.Empty;

    [ForeignKey("countries"), Required(ErrorMessage = "Id do país é obrigatório.")]
    public Guid CountryId { get; private set; }

    public virtual Country? Country { get; set; }
}
