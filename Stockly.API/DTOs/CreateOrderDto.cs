using System.ComponentModel.DataAnnotations;

namespace Stockly.API.DTOs;
public class CreateOrderDto
{
    [Required(ErrorMessage = "An order must contain at least one item.")]
    [MinLength(1, ErrorMessage = "An order must contain at least one item.")]
    public List<CreateOrderItemDto> Items { get; set; } = new();
}