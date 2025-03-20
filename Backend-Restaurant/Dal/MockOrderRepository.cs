using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class MockOrderRepository : IOrderRepository
    {
        private static List<Order> _orders = new List<Order>
        {
            new Order { Id = 1, Type = "Food", Description = "Burger", IsCompleted = false },
            new Order { Id = 2, Type = "Drink", Description = "Beer", IsCompleted = false },
            new Order { Id = 3, Type = "Food", Description = "Pizza", IsCompleted = false }
        };

        public List<Order> GetOrdersByType(string type)
        {
            return _orders.Where(o => o.Type == type).ToList();
        }

        public Order GetOrderById(int id)
        {
            return _orders.FirstOrDefault(o => o.Id == id);
        }

        public void AddOrder(Order order)
        {
            order.Id = _orders.Max(o => o.Id) + 1;
            _orders.Add(order);
        }

        public void UpdateOrder(Order order)
        {
            var existingOrder = _orders.FirstOrDefault(o => o.Id == order.Id);
            if (existingOrder != null)
            {
                existingOrder.Description = order.Description;
                existingOrder.IsCompleted = order.IsCompleted;
            }
        }

        public void DeleteOrder(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                _orders.Remove(order);
            }
        }

        public void CompleteOrder(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                order.IsCompleted = true;
            }
        }
    }
}