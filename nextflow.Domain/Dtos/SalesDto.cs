using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using nextflow.Domain.Enums;

namespace nextflow.Domain.Dtos;

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