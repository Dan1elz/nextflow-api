using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Enums;
using Nextflow.Domain.Exceptions;
using Nextflow.Domain.Interfaces.Models;
using Nextflow.Domain.Models.Base;

namespace Nextflow.Domain.Models;


[Table("orders")]
public class Order : BaseModel, IUpdatable<UpdateOrderDto>
{
    [ForeignKey("clients"), Required(ErrorMessage = "O Cliente é obrigatório.")]
    public Guid ClientId { get; private set; }
    public virtual Client? Client { get; private set; }

    [Required(ErrorMessage = "O status do pedido é obrigatório.")]
    public OrderStatus Status { get; private set; } = OrderStatus.PendingPayment;

    [Range(0.0, double.MaxValue, ErrorMessage = "O valor total não pode ser negativo."), Required(ErrorMessage = "O valor total é obrigatório.")]
    public decimal TotalAmount { get; private set; }
    [Range(0.0, double.MaxValue, ErrorMessage = "O valor do desconto não pode ser negativo.")]
    public decimal DiscountAmount { get; private set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; } = [];
    public virtual ICollection<Sale> Sales { get; set; } = [];

    public override string Preposition => "o";
    public override string Singular => "pedido";
    public override string Plural => "pedidos";

    private Order() : base() { }
    public Order(CreateOrderDto dto) : base()
    {
        ClientId = dto.ClientId;

    }
    public void Update(UpdateOrderDto dto)
    {
        // A lógica de atualização está nos hooks do UpdateOrderUseCase
        // Este método apenas marca a entidade como atualizada
        base.Update();
    }

    public void UpdateStatus(OrderStatus status)
    {
        Status = status;
        base.Update();
    }
    public new void Delete()
    {
        if (Status != OrderStatus.PendingPayment)
            throw new BadRequestException("Apenas pedidos com status 'Aguardando Pagamento' podem ser cancelados.");

        base.Delete();
        Status = OrderStatus.Canceled;
    }

    public void SetTotals(decimal totalAmount, decimal discountAmount)
    {
        TotalAmount = totalAmount;
        DiscountAmount = discountAmount;
    }
}
