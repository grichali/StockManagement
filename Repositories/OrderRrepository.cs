using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Order;
using api.Interfaces;
using api.Models;
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
            var orders = await _context.Order.Include(c => c.OrderItems).ToListAsync();

            return orders;
        }

        public async Task<Order?> Delete(int id)
        {
            var order = await _context.Order.FindAsync(id);

            if(order != null)
            {
                _context.Order.Remove(order);
                await _context.SaveChangesAsync();
                return order;
            }

            return null;
        }



        public async Task<Order?> GetOrderById(int id)
        {
            var order = await _context.Order
                .Include(c => c.OrderItems)
                    .ThenInclude(o => o.Product)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (order != null)
            {
                return order;
            }

            return null;
        }


        public async Task<List<Order>> GetOrdersByUser(int userId)
        {
            var orders = await _context.Order
                .Where(o => o.UserId == userId)
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
                    return null; 
                }

               Console.WriteLine($"hada lproduct : {product.Name}");


               Console.WriteLine($"Order Id is : {order.Id}");



                var orderitem = new OrderItems{
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    OrderId = order.Id,
                    Product = product 

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