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
        [Required(ErrorMessage = "Type is required.")]
        public string Type { get; set; } // food or drink
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        [Required]
        public string Status { get; set; } = "Pending"; // should this be saved in db?
    }
}
