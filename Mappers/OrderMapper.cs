using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Order;
using api.Dtos.OrderItems;
using api.Models;

namespace api.Mappers
{
    public static class OrderMapper
    {
        public static OrderDto ToOrderDto(this Order order){
            foreach(var OrderItem in order.OrderItems )
            {
                Console.WriteLine($"OrderItems id: {OrderItem.Id}");
                Console.WriteLine($"OrderItems name: {OrderItem.Product.Name}");
            }
            return new OrderDto{
                Id = order.Id,
                Date = order.Date,
                totalamount = order.amount,
                // Username = order.User.Name,
                OrderItems = order.OrderItems.Select( e => e.ToOrderItem()).ToList(),
            };
        }
    }
} 