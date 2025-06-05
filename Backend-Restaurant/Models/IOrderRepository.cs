using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetOrdersByType(string type);
        Task<List<Order>> GetOrdersByStatus(string status);
        Task<Order> GetOrderById(int id);
        Task AddOrder(Order order);
        Task UpdateOrder(Order order);
        Task DeleteOrder(int id);
        Task CompleteOrder(int id);
        Task<List<Order>> GetAllOrders();

    }
}
