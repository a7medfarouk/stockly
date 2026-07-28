using Stockly.API.DTOs;
using Stockly.API.Repositories.Interfaces;
using Stockly.API.Services.Interfaces;
using Stockly.API.Models;
using System.Runtime.CompilerServices;

namespace Stockly.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<OrderDto?> CreateOrderAsync(CreateOrderDto dto)
        {
            decimal totalAmount = 0;

            Order order = new();
            order.OrderDate = DateTime.Now;

            foreach (var item in dto.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);

                if (product == null)
                {
                    return null;
                }

                if (product.StockQuantity < item.Quantity)
                {
                    return null;
                }

                product.StockQuantity -= item.Quantity;

                await _productRepository.UpdateAsync(product);

                totalAmount += product.Price * item.Quantity;

                OrderItem orderItem = new();
                orderItem.ProductId = item.ProductId;
                orderItem.Quantity = item.Quantity;
                orderItem.UnitPrice = product.Price;

                order.OrderItems.Add(orderItem);
            }

            order.TotalAmount = totalAmount;
            var createdOrder = await _orderRepository.CreateAsync(order);
            OrderDto orderDto = new();
            orderDto.Id = createdOrder.Id;
            orderDto.OrderDate = createdOrder.OrderDate;
            orderDto.TotalAmount = createdOrder.TotalAmount;
            return orderDto;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            IEnumerable<Order> orders = await _orderRepository.GetAllAsync();
            List<OrderDto> orderDtos = new ();
            foreach (var order in orders)
            {
                orderDtos.Add(new OrderDto 
                {
                    Id = order.Id,
                    OrderDate = order.OrderDate,
                    TotalAmount = order.TotalAmount,
                });

            }
            return orderDtos;
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if(order == null)
            {
                return null;
            }
            OrderDto orderDto = new();
            OrderItemDto itemDto = new();
            orderDto.Id = order.Id;
            orderDto.OrderDate = order.OrderDate;
            orderDto.TotalAmount = order.TotalAmount;
            foreach (var item in order.OrderItems)
            {
                OrderItemDto orderItemDto = new();

                orderItemDto.ProductId = item.ProductId;
                orderItemDto.ProductName = item.Product.Name;
                orderItemDto.UnitPrice = item.UnitPrice;
                orderItemDto.Quantity = item.Quantity;

                orderDto.Items.Add(orderItemDto);
            }
            return orderDto;
        }
    }
}
