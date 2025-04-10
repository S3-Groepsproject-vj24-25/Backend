using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class OrderService:IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<Order> GetOrdersByType(string type)
        {
            return _orderRepository.GetOrdersByType(type);
        }

        public Order GetOrderById(int id)
        {
            return _orderRepository.GetOrderById(id);
        }

        public void AddOrder(Order order)
        {
            //default status = pending
            order.Status = "Pending";
            _orderRepository.AddOrder(order);
        }

        public void UpdateOrder(Order order)
        {
            _orderRepository.UpdateOrder(order);
        }

        public void DeleteOrder(int id)
        {
            _orderRepository.DeleteOrder(id);
        }

        public void StartOrderPreparation(int id)
        {
            var order = _orderRepository.GetOrderById(id);
            if (order != null)
            {
                order.Status = "Preparing";
                _orderRepository.UpdateOrder(order);
            }
        }

        public void CompleteOrder(int id)
        {
            var order = _orderRepository.GetOrderById(id);
            if (order != null)
            {
                order.Status = "Completed";
                order.IsCompleted = true;
                _orderRepository.UpdateOrder(order);
            }
        }

        public List<Order> GetOrdersByStatus(string status)
        {
            return _orderRepository.GetOrdersByStatus(status);
        }
    }
}
