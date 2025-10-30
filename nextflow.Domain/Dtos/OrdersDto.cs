
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Nextflow.Domain.Attributes;
using Nextflow.Domain.Dtos.Base;
using Nextflow.Domain.Enums;
using Nextflow.Domain.Models;

namespace Nextflow.Domain.Dtos;

public class CreateOrderDto : BaseDto
{
    [NotEmptyGuid(ErrorMessage = "O Usuário é obrigatório.")]
    public Guid UserId { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "O Id do cliente é obrigatório.")]
    public Guid ClientId { get; set; }
    public List<CreateOrderItemDto> Items { get; set; } = [];
}

public class CreateOrderItemDto : BaseDto
{
    [JsonIgnore] public Guid OrderId { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "Por favor, insira o ID do Produto.")]
    public Guid ProductId { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero."), Required(ErrorMessage = "A quantidade é obrigatória.")]
    public decimal Quantity { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "O desconto não pode ser negativo."), Required(ErrorMessage = "O desconto é obrigatório.")]
    public decimal Discount { get; set; }
}
public class UpdateOrderDto
{
    [NotEmptyGuid(ErrorMessage = "O Usuário é obrigatório.")]
    public Guid UserId { get; set; } = Guid.NewGuid();
    public List<UpdateOrderItemDto> Items { get; set; } = [];
}
public class UpdateOrderItemDto : CreateOrderItemDto { }
public class OrderResponseDto : BaseDto
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public ClientResponseDto? Client { get; set; }
    public OrderStatus Status { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public List<OrderItemResponseDto> OrderItems { get; set; } = [];
    public OrderResponseDto() { }
    public OrderResponseDto(Order entity)
    {
        Id = entity.Id;
        ClientId = entity.ClientId;
        Client = entity.Client != null ? new ClientResponseDto(entity.Client) : null;
        Status = entity.Status;
        TotalAmount = entity.TotalAmount;
        DiscountAmount = entity.DiscountAmount;
        OrderItems = [.. entity.OrderItems.Select(oi => new OrderItemResponseDto(oi))];
    }
}

public class OrderItemResponseDto : BaseDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public ProductResponseDto? Product { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalPrice { get; set; }

    public OrderItemResponseDto() { }
    public OrderItemResponseDto(OrderItem entity)
    {
        Id = entity.Id;
        OrderId = entity.OrderId;
        ProductId = entity.ProductId;
        Product = entity.Product != null ? new ProductResponseDto(entity.Product) : null;
        Quantity = entity.Quantity;
        UnitPrice = entity.UnitPrice;
        Discount = entity.Discount;
        TotalPrice = entity.TotalPrice;
    }
}