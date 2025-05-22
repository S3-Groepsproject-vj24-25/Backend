//using Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Dal
//{
//    public class MockOrderRepository : IOrderRepository
//    {
//        private static List<Order> _orders = new List<Order>
//{
//    new Order
//    {
//        Id = 1,
//        TableNumber = "5",
//        Status = "Pending",
//        IsCompleted = false,
//        Items = new List<OrderItem>
//        {
//            new OrderItem
//            {
//                Id = 1,
//                Name = "Burger",
//                Quantity = 1,
//                UnitPrice = 8.5m,
//                ItemTotal = 8.5m,
//                Type = "Food",
//                Modifications = new List<OrderItemModification>
//                {
//                    new OrderItemModification
//                    {
//                        Name= "Mod",
//                        Price=0.15m
//                    }
//                },
//                Instructions = "No onions"
//            }
//        }
//    },
//    new Order
//    {
//        Id = 2,
//        TableNumber = "3",
//        Status = "Pending",
//        IsCompleted = false,
//        Items = new List<OrderItem>
//        {
//            new OrderItem
//            {
//                Id = 2,
//                Name = "Beer",
//                Quantity = 2,
//                UnitPrice = 3.0m,
//                ItemTotal = 6.0m,
//                Type = "Drink",
//                Modifications = new List<OrderItemModification>
//                {
//                    new OrderItemModification
//                    {
//                        Name="Mod",
//                        Price=0.10m
//                    }
//                },
//                Instructions = "Cold"
//            }
//        }
//    },
//    new Order
//    {
//        Id = 3,
//        TableNumber = "2",
//        Status = "Pending",
//        IsCompleted = false,
//        Items = new List<OrderItem>
//        {
//            new OrderItem
//            {
//                Id = 3,
//                Name = "Pizza",
//                Quantity = 1,
//                UnitPrice = 10.0m,
//                ItemTotal = 10.0m,
//                Type = "Food",
//                Modifications = new List<OrderItemModification>
//                {
//                    new OrderItemModification
//                    {
//                        Name="Extra cheese",
//                        Price=0.5m
//                    }
//                },
//                Instructions = "Well done"
//            }
//        }
//    }
//};


//        public List<Order> GetOrdersByType(string type)
//        {
//            return _orders
//                .Where(o => o.Items.Any(i => i.Type.Equals(type, StringComparison.OrdinalIgnoreCase)))
//                .ToList();
//        }


//        public List<Order> GetOrdersByStatus(string status)
//        {
//            return _orders.Where(o => o.Status == status).ToList();
//        }

//        public Order GetOrderById(int id)
//        {
//            return _orders.FirstOrDefault(o => o.Id == id);
//        }

//        public void AddOrder(Order order)
//        {
//            order.Id = _orders.Max(o => o.Id) + 1;

//            int maxItemId = _orders.SelectMany(o => o.Items).DefaultIfEmpty().Max(i => i?.Id ?? 0);
//            foreach (var item in order.Items)
//            {
//                item.Id = ++maxItemId;
//                item.ItemTotal = item.UnitPrice * item.Quantity;
//            }

//            _orders.Add(order);
//        }


//        public void UpdateOrder(Order order)
//        {
//            var existingOrder = _orders.FirstOrDefault(o => o.Id == order.Id);
//            if (existingOrder != null)
//            {
//                existingOrder.TableNumber = order.TableNumber;
//                existingOrder.Status = order.Status;
//                existingOrder.IsCompleted = order.IsCompleted;

//                // Replace all items (simplified)
//                existingOrder.Items = order.Items.Select(item => new OrderItem
//                {
//                    Id = item.Id,
//                    Name = item.Name,
//                    Quantity = item.Quantity,
//                    UnitPrice = item.UnitPrice,
//                    ItemTotal = item.UnitPrice * item.Quantity,
//                    Modifications = item.Modifications,
//                    Instructions = item.Instructions,
//                    Type = item.Type
//                }).ToList();
//            }
//        }


//        public void DeleteOrder(int id)
//        {
//            var order = _orders.FirstOrDefault(o => o.Id == id);
//            if (order != null)
//            {
//                _orders.Remove(order);
//            }
//        }

//        public void CompleteOrder(int id)
//        {
//            var order = _orders.FirstOrDefault(o => o.Id == id);
//            if (order != null)
//            {
//                order.IsCompleted = true;
//            }
//        }
//    }
//}