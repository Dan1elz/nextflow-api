using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using nextflow.Domain.Dtos;
using nextflow.Domain.Enums;
using nextflow.Domain.Exceptions;
using nextflow.Domain.Models.Base;
using Nextflow.Domain.Models;

namespace nextflow.Domain.Models;

[Table("products")]
public class Product : BaseModel
{
    [ForeignKey("suppliers"), Required(ErrorMessage = "O Fornecedor é obrigatório.")]
    public Guid SupplierId { get; private set; }
    public virtual Supplier? Supplier { get; set; }

    [Required(ErrorMessage = "O código é obrigatório")]
    public string ProductCode { get; private set; } = string.Empty;

    [Required(ErrorMessage = "O nome é obrigatório"), StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres")]
    public string Name { get; private set; } = string.Empty;

    [Required(ErrorMessage = "A descrição é obrigatória"), StringLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres")]
    public string Description { get; private set; } = string.Empty;

    [StringLength(255, ErrorMessage = "O caminho da imagem não pode exceder 255 caracteres")]
    public string? Image { get; private set; } = string.Empty;

    [Required(ErrorMessage = "A quantidade em estoque é obrigatória"), Range(0, double.MaxValue, ErrorMessage = "A quantidade em estoque não pode ser negativa")]
    public decimal Quantity { get; private set; }

    [Required(ErrorMessage = "O tipo de unidade é obrigatório")]
    public UnitType UnitType { get; private set; }

    [Required(ErrorMessage = "O preço é obrigatório"), Range(0.0, double.MaxValue, ErrorMessage = "O preço não pode ser negativo")]
    public decimal Price { get; private set; }
    public DateOnly? Validity { get; private set; }
    public virtual ICollection<StockMovement> StockMovements { get; set; } = [];
    public virtual ICollection<CategoryProduct> CategoryProducts { get; set; } = [];

    private Product() : base() { }

    public Product(CreateProductDto dto) : base()
    {
        SupplierId = dto.SupplierId;
        ProductCode = dto.ProductCode;
        Name = dto.Name;
        Description = dto.Description;
        Quantity = dto.Quantity;
        UnitType = dto.UnitType;
        Price = dto.Price;
        Validity = dto.Validity;
    }
    public void Update(UpdateProductDto dto)
    {
        SupplierId = dto.SupplierId;
        ProductCode = dto.ProductCode;
        Name = dto.Name;
        Description = dto.Description;
        Quantity = dto.Quantity;
        UnitType = dto.UnitType;
        Price = dto.Price;
        Validity = dto.Validity;
        base.Update();
    }

    public void UpdateImage(string? imagePath)
    {
        Image = imagePath;
        base.Update();
    }
    public void RemoveImage()
    {
        Image = null;
        base.Update();
    }

    public void SetMovementStock(StockMovementDto dto)
    {
        if (dto.Quantity <= 0)
            throw new BadRequestException("A quantidade movimentada deve ser maior que zero.");

        switch (dto.MovementType)
        {
            case MovementType.Entry:
            case MovementType.Return:
                Quantity += dto.Quantity;
                break;
            case MovementType.Exit:
            case MovementType.Sales:
                if (dto.Quantity > Quantity)
                    throw new BadRequestException("A quantidade em estoque não pode ser negativa.");
                Quantity -= dto.Quantity;
                break;
            case MovementType.Adjustment:
                Quantity = dto.Quantity;
                break;
            default:
                throw new BadRequestException("Tipo de movimento inválido.");
        }
    }
}
