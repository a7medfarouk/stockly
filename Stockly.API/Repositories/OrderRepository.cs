using Microsoft.EntityFrameworkCore;
using Stockly.API.Data;
using Stockly.API.Models;
using Stockly.API.Repositories.Interfaces;


namespace Stockly.API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Order> CreateAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(order => order.OrderItems)
                .ThenInclude(orderitem => orderitem.Product)
                .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders
                .Include(orders => orders.OrderItems)
                .ThenInclude(orderitem => orderitem.Product)
                .FirstOrDefaultAsync(order => order.Id == id);
        }
    }
}
