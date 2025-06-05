using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _orderRepository.GetAllOrders();
        }


        public async Task<List<Order>> GetOrdersByType(string type)
        {
            return await _orderRepository.GetOrdersByType(type);
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _orderRepository.GetOrderById(id);
        }

        public async Task AddOrder(Order order)
        {
            order.Status = "Pending";
            foreach (var item in order.Items)
            {
                item.ItemTotal = item.UnitPrice * item.Quantity;
            }

            await _orderRepository.AddOrder(order);
        }

        public async Task UpdateOrder(Order order)
        {
            await _orderRepository.UpdateOrder(order);
        }

        public async Task DeleteOrder(int id)
        {
            await _orderRepository.DeleteOrder(id);
        }

        public async Task StartOrderPreparation(int id)
        {
            var order = await _orderRepository.GetOrderById(id);
            if (order != null)
            {
                order.Status = "Preparing";
                await _orderRepository.UpdateOrder(order);
            }
        }

        public async Task CompleteOrder(int id)
        {
            var order = await _orderRepository.GetOrderById(id);
            if (order != null)
            {
                order.Status = "Completed";
                order.IsCompleted = true;
                await _orderRepository.UpdateOrder(order);
            }
        }

        public async Task<List<Order>> GetOrdersByStatus(string status)
        {
            return await _orderRepository.GetOrdersByStatus(status);
        }
    }
}
