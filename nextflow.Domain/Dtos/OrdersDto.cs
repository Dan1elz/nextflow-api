
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace nextflow.Domain.Dtos;

public class CreateOrderDto
{
    [Required(ErrorMessage = "O Id do cliente é obrigatório.")]
    public Guid ClientId { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "O valor total não pode ser negativo."), Required(ErrorMessage = "O valor total é obrigatório.")]
    public decimal TotalAmount { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "O valor do desconto não pode ser negativo.")]
    public decimal DiscountAmount { get; set; }
    public List<CreateOrderItemDto> Items { get; set; } = [];
}

public class CreateOrderItemDto
{
    [JsonIgnore] public Guid OrderId { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "Por favor, insira o ID do Produto.")]
    public Guid ProductId { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero."), Required(ErrorMessage = "A quantidade é obrigatória.")]
    public decimal Quantity { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "O preço unitário não pode ser negativo."), Required(ErrorMessage = "O preço unitário é obrigatório.")]
    public decimal UnitPrice { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "O desconto não pode ser negativo."), Required(ErrorMessage = "O desconto é obrigatório.")]
    public decimal Discount { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "O valor total não pode ser negativo."), Required(ErrorMessage = "O valor total é obrigatório.")]
    public decimal TotalPrice { get; set; }
}