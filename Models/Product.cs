using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{ 
    public class Product
    {

        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public float Price { get; set; }


        public int Quantity { get; set; }

// one to many with category
        public int? CategoryId { get; set; }
        
        public Category?  Category { get; set; }
//end 

// many to one with orderitems
        public List<OrderItems> OrderItems { get; set; } = new List<OrderItems>();
//end

    }
}