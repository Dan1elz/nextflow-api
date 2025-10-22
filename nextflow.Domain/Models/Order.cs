using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using nextflow.Domain.Dtos;
using nextflow.Domain.Enums;
using nextflow.Domain.Exceptions;
using nextflow.Domain.Interfaces.Models;
using nextflow.Domain.Models.Base;
using Nextflow.Domain.Models;

namespace nextflow.Domain.Models;

[Table("orders")]
public class Order : BaseModel, IUpdatable<OrderStatus>
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

    private Order() : base() { }
    public Order(CreateOrderDto dto) : base()
    {
        ClientId = dto.ClientId;

    }
    public void Update(OrderStatus status)
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
