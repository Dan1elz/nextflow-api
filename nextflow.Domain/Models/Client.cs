using Nextflow.Domain.Interfaces.Models;
using Nextflow.Domain.Models.Base;
using Nextflow.Domain.Dtos;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nextflow.Domain.Models;

[Table("clients")]
public class Client : Person, IUpdatable<UpdateClientDto>
{
    public virtual ICollection<Address> Addresses { get; set; } = [];
    public virtual ICollection<Contact> Contacts { get; set; } = [];
    public virtual ICollection<Order> Orders { get; set; } = [];

    public override string Preposition => "o";
    public override string Singular => "cliente";
    public override string Plural => "clientes";
    private Client() : base() { }

    public Client(CreateClientDto dto) : base(dto) { }

    public void Update(UpdateClientDto dto)
    {
        base.Update(dto);
    }
}
