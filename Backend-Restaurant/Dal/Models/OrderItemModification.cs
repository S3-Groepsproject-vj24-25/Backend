using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Models
{
    public class OrderItemModification
    {
        public int Id { get; set; }     // maps to Modifications.ID
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
