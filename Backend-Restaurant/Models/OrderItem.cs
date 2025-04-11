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
        public decimal Price { get; set; }
        public string Modifications { get; set; }
        public string Instructions { get; set; }
        public decimal TotalPrice { get; set; }
        [Required(ErrorMessage = "Type is required.")]
        public string Type { get; set; } //food or drink
    }
}
