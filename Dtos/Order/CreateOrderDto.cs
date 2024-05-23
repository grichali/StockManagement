using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.OrderItems;

namespace api.Dtos.Order
{
    public class CreateOrderDto
    {        
        public string UserId { get; set; }

        public List<CreateOrderItemDTO> Items { get; set; } = new List<CreateOrderItemDTO>();
    }
}