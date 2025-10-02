using nextflow.Domain.Models.Base;
using Nextflow.Domain.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nextflow.Domain.Models;

[Table("clients")]
public class Client : Person
{   
    private Client() : base() { }

    public Client(CreateClientDto dto) : base(dto) {}

    public void Update(UpdateClientDto dto) 
    {
        base.Update(dto);
    }
}
