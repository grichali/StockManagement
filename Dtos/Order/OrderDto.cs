using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.OrderItems;

namespace api.Dtos.Order
{
    public class OrderDto
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        
        public float totalamount { get; set; }

        public String? Username { get; set; }

        public List<OrderItemsDto> OrderItems { get; set; } = new List<OrderItemsDto>();  
    }
}