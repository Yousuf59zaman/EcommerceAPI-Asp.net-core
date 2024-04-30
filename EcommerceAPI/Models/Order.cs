using EcommerceAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public int UserId { get; set; }

        public string Status { get; set; } = "Pending";

        [Required]
        public DateTime OrderDate { get; set; }

        public User User { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}

