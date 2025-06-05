using Microsoft.EntityFrameworkCore;
using Models;

namespace Dal
{
    public class OrderRepository : IOrderRepository
    {
        private readonly RestaurantDbContext _context;

        public OrderRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderMenuItems)
                    .ThenInclude(omi => omi.MenuItem)
                        .ThenInclude(mi => mi.ModificationMenuItems)
                            .ThenInclude(mmi => mmi.Modification)
                .ToListAsync();

            return MapToModel(orders);
        }

        public async Task<List<Order>> GetOrdersByType(string type)
        {
            var orders = await _context.Orders
                .Include(o => o.OrderMenuItems)
                    .ThenInclude(omi => omi.MenuItem)
                        .ThenInclude(mi => mi.ModificationMenuItems)
                            .ThenInclude(mmi => mmi.Modification)
                .Where(o => o.OrderMenuItems.Any(omi => omi.MenuItem.Category == type))
                .ToListAsync();

            return MapToModel(orders);
        }

        public async Task<List<Order>> GetOrdersByStatus(string status)
        {
            bool isPaid = status.Equals("Paid", StringComparison.OrdinalIgnoreCase);

            var orders = await _context.Orders
                .Include(o => o.OrderMenuItems)
                    .ThenInclude(omi => omi.MenuItem)
                        .ThenInclude(mi => mi.ModificationMenuItems)
                            .ThenInclude(mmi => mmi.Modification)
                .Where(o => o.Paid == isPaid)
                .ToListAsync();

            return MapToModel(orders);
        }

        public async Task<Order> GetOrderById(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderMenuItems)
                    .ThenInclude(omi => omi.MenuItem)
                        .ThenInclude(mi => mi.ModificationMenuItems)
                            .ThenInclude(mmi => mmi.Modification)
                .FirstOrDefaultAsync(o => o.ID == id);

            return MapToModel(order);
        }

        public async Task AddOrder(Order order)
        {
            var dbOrder = new Dal.Models.Order
            {
                TableID = int.Parse(order.TableNumber),
                Paid = false,
                TotalCost = order.Items.Sum(i => i.ItemTotal),
                Timestamp = DateTime.UtcNow,
                OrderMenuItems = order.Items.Select(item => new Dal.Models.OrderMenuItem
                {
                    MenuItemID = item.Id,
                    Amount = item.Quantity,
                    Notes = item.Instructions
                }).ToList()
            };

            _context.Orders.Add(dbOrder);
            await _context.SaveChangesAsync();

            order.Id = dbOrder.ID;
        }

        public async Task UpdateOrder(Order order)
        {
            var existingOrder = await _context.Orders
                .Include(o => o.OrderMenuItems)
                .FirstOrDefaultAsync(o => o.ID == order.Id);

            if (existingOrder == null) return;

            existingOrder.TableID = int.Parse(order.TableNumber);
            existingOrder.Paid = order.IsCompleted;
            existingOrder.TotalCost = order.Items.Sum(i => i.ItemTotal);

            _context.OrderMenuItems.RemoveRange(existingOrder.OrderMenuItems);

            existingOrder.OrderMenuItems = order.Items.Select(item => new Dal.Models.OrderMenuItem
            {
                MenuItemID = item.Id,
                Amount = item.Quantity,
                Notes = item.Instructions
            }).ToList();

            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderMenuItems)
                .FirstOrDefaultAsync(o => o.ID == id);

            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CompleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                order.Paid = true;
                await _context.SaveChangesAsync();
            }
        }

        private List<Order> MapToModel(List<Dal.Models.Order> dbOrders) =>
            dbOrders.Select(MapToModel).ToList();

        private Order MapToModel(Dal.Models.Order dbOrder)
        {
            if (dbOrder == null) return null;

            return new Order
            {
                Id = dbOrder.ID,
                TableNumber = dbOrder.TableID.ToString(),
                IsCompleted = dbOrder.Paid,
                Status = dbOrder.Paid ? "Paid" : "Pending",
                Items = dbOrder.OrderMenuItems?.Select(omi => new OrderItem
                {
                    Id = omi.MenuItemID,
                    Name = omi.MenuItem?.ProductName,
                    Quantity = omi.Amount,
                    UnitPrice = omi.MenuItem?.Price ?? 0,
                    ItemTotal = (omi.MenuItem?.Price ?? 0) * omi.Amount,
                    Type = omi.MenuItem?.Category,
                    Instructions = omi.Notes,
                    Modifications = omi.MenuItem?.ModificationMenuItems?.Select(mmi => new OrderItemModification
                    {
                        Id = mmi.ModificationID,
                        Name = mmi.Modification?.Name,
                        Price = mmi.Modification?.Price ?? 0
                    }).ToList()
                }).ToList()
            };
        }
    }
}
