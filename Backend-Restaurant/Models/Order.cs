﻿using System;
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
        public int TableNumber { get; set; }
        [Required(ErrorMessage = "Type is required.")]
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public bool IsCompleted { get; set; }
        [Required]
        public string Status { get; set; } = "Pending"; // should this be saved in db?
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
