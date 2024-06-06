using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Order;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class OrderRrepository : IOrderRepository
    {

        private readonly ApplicationDbContext _context;

        public OrderRrepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersAll()
        {
            var orders = await _context.Order.Include(u => u.User).Include(e => e.OrderItems).ThenInclude(p => p.Product).ToListAsync();
            return orders;
        }

        public async Task<Order?> Delete(int id)
        {
            var order = await _context.Order.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id);

            if(order != null)
            {
                foreach (var orderItem in order.OrderItems)
                {
                    _context.OrderItems.Remove(orderItem);
                }
                _context.Order.Remove(order);
                await _context.SaveChangesAsync();
                return order;
            }

            return null;
        }



        public async Task<Order?> GetOrderById(int id)
        {
            var order = await _context.Order
                .Include(u => u.User).Include(e => e.OrderItems).ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (order != null)
            {
                return order;
            }

            return null;
        }


        public async Task<List<Order>> GetOrdersByUser(string userId)
        {
            var orders = await _context.Order
                .Where(o => o.UserId.Equals(userId))
                .Include(u => u.User).Include(e => e.OrderItems).ThenInclude(p => p.Product)
                .ToListAsync();

            return orders;
        } 

          public async Task<Order?> CreateOrder(CreateOrderDto orderDto)
        {
            var order = new Order{
                Date = DateTime.Now,
                UserId = orderDto.UserId,
            };

            _context.Order.Add(order);
            await _context.SaveChangesAsync();
            float Totamount = 0 ;

            foreach(var item in orderDto.Items)
            {
                var product = await _context.Product.FindAsync(item.ProductId);

                if(product == null){
                    Console.WriteLine($"no prod");
                    throw new Exception("Product not found"); 
                }

                if(item.Quantity > product.Quantity)
                {
                    throw new Exception("No enough Products");
                }


               Console.WriteLine($"This is product : {product.Name}");


               Console.WriteLine($"Order Id is : {order.Id}");



                var orderitem = new OrderItems{
                    Quantity = item.Quantity,
                    Product = product,
                    ProductId = product.Id,
                    Order = order,
                };

                _context.OrderItems.Add(orderitem);

                product.Quantity -= item.Quantity;  
                Totamount += item.Quantity * product.Price;

            }

            order.amount = Totamount;

            await _context.SaveChangesAsync();

            return order;
        }
    }
}