using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Category 
    {
        [Key]
        public int id { get; set; }

        public string Name { get; set; }    = string.Empty;

        public string  Description { get; set; }    = string.Empty;

        public List<Product> Products { get; set; } = new List<Product>();
    }
}