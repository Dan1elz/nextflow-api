using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Models.Base;

namespace Nextflow.Domain.Models;

[Table("order_items")]
public class OrderItem : BaseModel
{
    [ForeignKey("orders"), Required(ErrorMessage = "A Ordem de Venda é obrigatória.")]
    public Guid OrderId { get; private set; }
    public virtual Order? Order { get; set; }

    [ForeignKey("products"), Required(ErrorMessage = "O Produto é obrigatório.")]
    public Guid ProductId { get; private set; }
    public virtual Product? Product { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero."), Required(ErrorMessage = "A quantidade é obrigatória.")]
    public decimal Quantity { get; private set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "O preço unitário não pode ser negativo."), Required(ErrorMessage = "O preço unitário é obrigatório.")]
    public decimal UnitPrice { get; private set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "O desconto não pode ser negativo."), Required(ErrorMessage = "O desconto é obrigatório.")]
    public decimal Discount { get; private set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "O valor total não pode ser negativo."), Required(ErrorMessage = "O valor total é obrigatório.")]
    public decimal TotalPrice { get; private set; }

    private OrderItem() : base() { }

    public OrderItem(CreateOrderItemDto dto) : base()
    {
        OrderId = dto.OrderId;
        ProductId = dto.ProductId;
        Quantity = dto.Quantity;
        Discount = dto.Discount;
    }

    public void SetPricing(decimal unitPrice, decimal totalPrice)
    {
        UnitPrice = unitPrice;
        TotalPrice = totalPrice;
    }
}