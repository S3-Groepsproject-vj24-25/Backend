using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrdersByType(string type);
        Task<Order> GetOrderById(int id);
        Task<List<Order>> GetOrdersByStatus(string status);
        Task StartOrderPreparation(int id);
        Task AddOrder(Order order);
        Task UpdateOrder(Order order);
        Task DeleteOrder(int id);
        Task CompleteOrder(int id);
    }
}
