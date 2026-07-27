using Stockly.API.Models;

namespace Stockly.API.Repositories.Interfaces
{
    public interface IOrderRepository 
    {
        Task<IEnumerable<Order>> GetAllAsync();

        Task<Order?> GetByIdAsync(int id);

        Task<Order> CreateAsync(Order order);
    }
}
