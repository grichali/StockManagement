using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace api.Dtos.Product
{
    public class UpdateProductDto
    {
        public string Name { get; set; }

        public float Price { get; set; }

        
        public int Quantity { get; set; }

        public int CategoryId { get; set; }      
    }
}