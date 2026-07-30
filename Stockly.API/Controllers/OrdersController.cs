using Microsoft.AspNetCore.Mvc;
using Stockly.API.DTOs;
using Stockly.API.Services.Interfaces;

namespace Stockly.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;


        public OrdersController(IOrderService OrderService)
        {
            _orderService = OrderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder(CreateOrderDto orderDto)
        {
            var createdOrderDto = await _orderService.CreateOrderAsync(orderDto);
            if (createdOrderDto == null)
            {
                return NotFound();
            }
            return CreatedAtAction(
                nameof(GetOrderById),
                new { id = createdOrderDto.Id },
                createdOrderDto);
        }
    }
}