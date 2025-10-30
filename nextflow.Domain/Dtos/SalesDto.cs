using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Nextflow.Domain.Enums;
using Nextflow.Domain.Models;

namespace Nextflow.Domain.Dtos;

public class CreateSaleDto
{
    [Required(ErrorMessage = "O Usuário é obrigatório.")]
    public Guid UserId { get; set; }

    [Required(ErrorMessage = "A Ordem de Venda é obrigatória.")]
    public Guid OrderId { get; set; }

    public List<CreatePaymentDto> Payments { get; set; } = [];
}

public class CreatePaymentDto
{
    [JsonIgnore] public Guid SaleId { get; set; } = Guid.NewGuid();

    [Range(0.01, double.MaxValue, ErrorMessage = "O valor do pagamento deve ser maior que zero."), Required(ErrorMessage = "O valor do pagamento é obrigatório.")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "O método de pagamento é obrigatório.")]
    public PaymentMethod Method { get; set; }
}
public class SaleResponseDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public UserResponseDto? User { get; set; }
    public Guid OrderId { get; set; }
    public OrderResponseDto? Order { get; set; }
    public List<PaymentResponseDto> Payments { get; set; } = [];

    public SaleResponseDto() { }

    public SaleResponseDto(Sale entity)
    {
        Id = entity.Id;
        UserId = entity.UserId;
        User = entity.User != null ? new UserResponseDto(entity.User) : null;
        OrderId = entity.OrderId;
        Order = entity.Order != null ? new OrderResponseDto(entity.Order) : null;
        Payments = [.. entity.Payments.Select(p => new PaymentResponseDto(p))];
    }
}
public class PaymentResponseDto
{
    public Guid Id { get; set; }
    public Guid SaleId { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; }

    public PaymentResponseDto() { }

    public PaymentResponseDto(Payment entity)
    {
        Id = entity.Id;
        SaleId = entity.SaleId;
        Amount = entity.Amount;
        Method = entity.Method;
    }
}
