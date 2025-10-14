using nextflow.Domain.Models.Base;
using Nextflow.Domain.Dtos;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nextflow.Domain.Models;

[Table("clients")]
public class Client : Person
{
    public virtual ICollection<Address> Addresses { get; set; } = [];
    public virtual ICollection<Contact> Contacts { get; set; } = [];
    private Client() : base() { }

    public Client(CreateClientDto dto) : base(dto) { }

    public void Update(UpdateClientDto dto)
    {
        base.Update(dto);
    }
}
