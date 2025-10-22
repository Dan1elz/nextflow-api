using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using nextflow.Domain.Dtos;
using nextflow.Domain.Enums;
using nextflow.Domain.Models.Base;

namespace nextflow.Domain.Models;

[Table("payments")]
public class Payment : BaseModel
{
    [ForeignKey("sales"), Required(ErrorMessage = "A Venda é obrigatória.")]
    public Guid SaleId { get; private set; }
    public virtual Sale? Sale { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "O valor do pagamento deve ser maior que zero."), Required(ErrorMessage = "O valor do pagamento é obrigatório.")]
    public decimal Amount { get; private set; }

    [Required(ErrorMessage = "O método de pagamento é obrigatório.")]
    public PaymentMethod Method { get; private set; }

    private Payment() : base() { }

    public Payment(CreatePaymentDto dto) : base()
    {
        SaleId = dto.SaleId;
        Amount = dto.Amount;
        Method = dto.Method;
    }
}
