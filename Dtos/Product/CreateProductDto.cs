using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Product
{
    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;

        public float Price { get; set; }


        public int Quantity { get; set; }

        public int CategoryId { get; set; }     
    }
}