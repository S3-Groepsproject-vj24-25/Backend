using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        //public string Modifications { get; set; }
        public string Instructions { get; set; }
        public List<OrderItemModification> Modifications { get; set; } = new List<OrderItemModification>();
        //public string Instructions { get; set; }
        public decimal ItemTotal { get; set; }
        [Required(ErrorMessage = "Type is required.")]
        public string Type { get; set; } //food or drink
        //public string ImageUrl { get; set; }
    }

    public class OrderItemModification
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
