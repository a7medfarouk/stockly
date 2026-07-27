using System.ComponentModel.DataAnnotations;

namespace Stockly.API.DTOs;
public class UpdateProductDto
{
    [Required(ErrorMessage = "Product Name Is Required!")]
    [StringLength(100, ErrorMessage = "Product Name Cannot Exceed 100 Characters.")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Product Description cannot Exceed 500 Characters.")]
    public string? Description { get; set; }

    [Range(0.01, 999999.99, ErrorMessage = "Product price must be between 0.01 and 999999.99.")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Product Stock Cant be Negative.")]
    public int StockQuantity { get; set; }
}