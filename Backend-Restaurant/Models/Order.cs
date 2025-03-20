using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Type { get; set; } // food or drink
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
