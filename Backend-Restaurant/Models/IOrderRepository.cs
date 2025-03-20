using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public interface IOrderRepository
    {
        List<Order> GetOrdersByType(string type);
        Order GetOrderById(int id);
        void AddOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(int id);
        void CompleteOrder(int id);
    }
}
