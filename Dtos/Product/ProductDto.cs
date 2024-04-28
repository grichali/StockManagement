using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Category;
using api.Models;
namespace api.Dtos.Product
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public float Price { get; set; }


        public int Quantity { get; set; }
        
        public CategoryDto? categoryDto { get; set; }
    }
}