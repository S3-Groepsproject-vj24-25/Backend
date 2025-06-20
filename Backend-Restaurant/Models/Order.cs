using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderId { get; set; } 
        public string TableID { get; set; }
        [Required(ErrorMessage = "Type is required.")]
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public bool IsCompleted { get; set; }
        [Required]
        public string Status { get; set; } = "Pending"; // should this be saved in db?
        public DateTime Timestamp { get; set; }
    }

    public class OrderSummary
    {
        public decimal Subtotal { get; set; }
        public decimal Additions { get; set; }
        public decimal Total { get; set; }
    }
}
