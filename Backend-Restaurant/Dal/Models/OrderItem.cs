using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Models
{
    public class OrderItem
    {
        public int Id { get; set; }  // maps to MenuItem.ID
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal ItemTotal { get; set; }
        public string Type { get; set; } // Food/Drink etc.
        public string Instructions { get; set; }

        public List<OrderItemModification> Modifications { get; set; } = new();
    }
}
