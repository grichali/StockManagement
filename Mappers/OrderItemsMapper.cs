using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Models;

namespace api.Mappers
{
    public static class OrderItemsMapper
    {
        public static OrderItemsDto ToOrderItem(this OrderItems orderItems){
            return new OrderItemsDto{
                Id = orderItems.Id,
                Quantity = orderItems.Quantity,
                ProductName = orderItems.Product.Name,
                ProductPrice = orderItems.Product.Price,
                amount = orderItems.Quantity * orderItems.Product.Price,
            };
        }
    }
}