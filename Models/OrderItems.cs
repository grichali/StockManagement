using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class OrderItems
    {
        [Key]
        public int Id { get; set; } 

        public int Quantity { get; set; }


// one to many with product 
        public int? ProductId { get; set; }

        public Product Product { get; set; }

// one to many with orders 
        public int? OrderId { get; set; }

        public Order? Order { get; set; }
    }
} 