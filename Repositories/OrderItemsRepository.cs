using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.OrderItems;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class OrderItemsRepository : IOrderItemsRepository
    {

        private readonly ApplicationDbContext _context;

        public OrderItemsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderItems>> CreateOrderItems(List<CreateOrderItemDTO> itemDTOs, int orderid)
        {
            // var order = await _context.Order.FindAsync(orderid);

            // if (order != null)
            // {
            //     float Totamount = 0 ;
            //     foreach(var item in itemDTOs)
            //     {
            //         var product = await _context.Product.FindAsync(item.ProductId);

            //         if(product == null){
            //             return []; 
            //         }

            //         var orderitem = new OrderItems{
            //             ProductId = product.Id,
            //             Quantity = item.Quantity,
            //             OrderId = order.Id
            //         };

            //         _context.OrderItems.Add(orderitem);

            //         product.Quantity -= item.Quantity;  
            //         Totamount += item.Quantity * product.Price;
            //         order.amount = Totamount;
            //     }

            //     await _context.SaveChangesAsync();
            //     return await _context.OrderItems.Where(x => x.OrderId == order.Id).ToListAsync();
            // }

            return [];

        }
    }
}