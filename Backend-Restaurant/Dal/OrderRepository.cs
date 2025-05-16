//using Models;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace Dal
//{
//    public class OrderRepository : IOrderRepository
//    {
//        private readonly RestaurantDbContext _context;

//        public OrderRepository(RestaurantDbContext context)
//        {
//            _context = context;
//        }

//        public void AddOrder(Order order)
//        {
//            foreach (var item in order.Items)
//            {
//                item.TotalPrice = item.Price * item.Quantity;
//            }

//            _context.Orders.Add(order);
//            _context.SaveChanges();
//        }

//        public void DeleteOrder(int id)
//        {
//            var order = _context.Orders
//                .Include(o => o.Items)
//                .FirstOrDefault(o => o.Id == id);

//            if (order != null)
//            {
//                _context.OrderMenuItems.RemoveRange(order.Items);
//                _context.Orders.Remove(order);
//                _context.SaveChanges();
//            }
//        }

//        public Order GetOrderById(int id)
//        {
//            return _context.Orders
//                .Include(o => o.Items)
//                .FirstOrDefault(o => o.Id == id);
//        }

//        public List<Order> GetOrdersByStatus(string status)
//        {
//            return _context.Orders
//                .Include(o => o.Items)
//                .Where(o => o.Status == status)
//                .ToList();
//        }

//        public List<Order> GetOrdersByType(string type)
//        {
//            return _context.Orders
//                .Include(o => o.Items)
//                .Where(o => o.Items.Any(i => i.Type == type))
//                .ToList();
//        }

//        public void UpdateOrder(Order order)
//        {
//            var existing = _context.Orders
//                .Include(o => o.Items)
//                .FirstOrDefault(o => o.Id == order.Id);

//            if (existing != null)
//            {
//                existing.TableNumber = order.TableNumber;
//                existing.Status = order.Status;
//                existing.IsCompleted = order.IsCompleted;

//                // Remove existing items
//                _context.OrderMenuItems.RemoveRange(existing.Items);

//                // Add updated items
//                foreach (var item in order.Items)
//                {
//                    item.TotalPrice = item.Price * item.Quantity;
//                    existing.Items.Add(item);
//                }

//                _context.SaveChanges();
//            }
//        }

//        public void CompleteOrder(int id)
//        {
//            var order = _context.Orders.FirstOrDefault(o => o.Id == id);
//            if (order != null)
//            {
//                order.IsCompleted = true;
//                order.Status = "Completed";
//                _context.SaveChanges();
//            }
//        }
//    }
//}
