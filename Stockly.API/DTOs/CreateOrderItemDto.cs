using System.ComponentModel.DataAnnotations;

namespace Stockly.API.DTOs;
public class CreateOrderItemDto
{
    [Range(1, int.MaxValue, ErrorMessage = "Please select a valid product.")]
    public int ProductId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    public int Quantity { get; set; }
}