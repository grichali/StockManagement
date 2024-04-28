using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Order;
using api.Models;

namespace api.Mappers
{
    public static class OrderMapper
    {
        public static OrderDto ToOrderDto(this Order order){
            return new OrderDto{
                Id = order.Id,
                Date = order.Date,
                totalamount = order.amount,
                Username = order.User.Name,
                OrderItems = order.OrderItems.Select( e => e.ToOrderItem()).ToList(),
            };
        }
    }
}