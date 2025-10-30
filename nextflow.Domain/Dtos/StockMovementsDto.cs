using System.ComponentModel.DataAnnotations;
using Nextflow.Domain.Attributes;
using Nextflow.Domain.Dtos.Base;
using Nextflow.Domain.Enums;
using Nextflow.Domain.Models;

namespace Nextflow.Domain.Dtos;

public class StockMovementDto : BaseDto
{
    [NotEmptyGuid(ErrorMessage = "O Produto é obrigatório.")]
    public Guid ProductId { get; set; }

    [Required(ErrorMessage = "A quantidade é obrigatória."), Range(0.01, double.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
    public decimal Quantity { get; set; }

    [Required(ErrorMessage = "O tipo de movimento é obrigatório.")]
    public MovementType MovementType { get; set; }

    [StringLength(255, ErrorMessage = "A descrição não pode exceder 255 caracteres.")]
    public string? Description { get; set; } = string.Empty;
}

public class CreateStockMovementDto : StockMovementDto
{
    [NotEmptyGuid(ErrorMessage = "O Usuário é obrigatório.")]
    public Guid UserId { get; set; }

    [Required(ErrorMessage = "A cotação é obrigatória."), Range(0.0, double.MaxValue, ErrorMessage = "A cotação não pode ser negativa.")]
    public decimal Quotation { get; set; }

    public CreateStockMovementDto() : base() { }

    public CreateStockMovementDto(StockMovementDto dto, Guid userId, decimal quotation) : base()
    {
        ProductId = dto.ProductId;
        Quantity = dto.Quantity;
        MovementType = dto.MovementType;
        Description = dto.Description;
        UserId = userId;
        Quotation = quotation;
    }
}

public class StockMovementResponseDto : BaseDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public ProductResponseDto? Product { get; set; }
    public decimal Quantity { get; set; }
    public MovementType MovementType { get; set; }
    public string? Description { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public UserResponseDto? User { get; set; }
    public decimal Quotation { get; set; }

    public StockMovementResponseDto() { }

    public StockMovementResponseDto(StockMovement entity)
    {
        ProductId = entity.ProductId;
        Product = entity.Product != null ? new ProductResponseDto(entity.Product) : null;
        Quantity = entity.Quantity;
        MovementType = entity.MovementType;
        Description = entity.Description;
        UserId = entity.UserId;
        User = entity.User != null ? new UserResponseDto(entity.User) : null;
        Quotation = entity.Quotation;
    }
}