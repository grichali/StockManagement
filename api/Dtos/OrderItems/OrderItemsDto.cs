using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
    public class OrderItemsDto
    {
        public int Id { get; set; } 

        public int Quantity { get; set; }

        public String? ProductName { get; set; }

        public float ProductPrice { get; set; }


        public float? amount { get; set; }


    }
}