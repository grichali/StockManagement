using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Category;
using api.Dtos.Order;
using api.Dtos.Product;
using api.Dtos.Statistics;
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
            var orders = await _context.Order.Include(e => e.OrderItems).ThenInclude(p => p.Product).ToListAsync();
            return orders;
        }

        public async Task<Order?> GetOrderById(int id)
        {
            var order = await _context.Order
                .Include(e => e.OrderItems).ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (order != null)
            {
                return order;
            }

            return null;
        }


          public async Task<Order?> CreateOrder(CreateOrderDto orderDto)
        {
            var order = new Order{
                Date = DateTime.Now,
                
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

        public async Task<GlobalStatisticsDto> GetStatistics(DateTime startDate, DateTime endDate)
        {
            var orders = await _context.Order
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ThenInclude(p => p.Category)
                .Where(o => o.Date >= startDate.Date && o.Date < endDate.Date.AddDays(1))
                .ToListAsync();
            var productStatistics = new Dictionary<int, ProductStatisticsDto>();
            var categoryStatistics = new Dictionary<int, CategoryStatisticsDto>();
            decimal totalProfit = 0m;
            foreach (var order in orders)
                {
                    foreach (var orderItem in order.OrderItems)
                    {
                        var product = orderItem.Product;
                        var category = product.Category;

                        if (!productStatistics.ContainsKey(product.Id))
                        {
                            productStatistics[product.Id] = new ProductStatisticsDto
                            {
                                ProductId = product.Id,
                                ProductName = product.Name,
                                QuantitySold = 0,
                                TotalProfit = 0m
                            };
                        }

                        productStatistics[product.Id].QuantitySold += orderItem.Quantity;
                        productStatistics[product.Id].TotalProfit += (orderItem.Quantity * (decimal)(product.Price - product.BuyPrice));

                        if (!categoryStatistics.ContainsKey(category.id))
                        {
                            categoryStatistics[category.id] = new CategoryStatisticsDto
                            {
                                CategoryId = category.id,
                                CategoryName = category.Name,
                                TotalProfit = 0m
                            };
                        }

                        categoryStatistics[category.id].TotalProfit += (orderItem.Quantity * (decimal)(product.Price - product.BuyPrice));
                        decimal profit = (decimal)(orderItem.Product.Price - orderItem.Product.BuyPrice) * orderItem.Quantity;
                        totalProfit += profit;
                    }
                    }

                    return new GlobalStatisticsDto
                    {
                        ProductStatistics = productStatistics.Values.ToList(),
                        CategoryStatistics = categoryStatistics.Values.ToList(),
                        TotalProfit = totalProfit 
                    };
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

        public async Task<int> totalOrders()
        {
            int totalOrders = await _context.Order.CountAsync();
            return totalOrders;
        }

        public async Task<List<TopProduct>> TopProducts()
        {
            var topProducts = await _context.OrderItems
                .Include(oi => oi.Product)
                .GroupBy(i => i.ProductId)
                .Select(g => new
                {
                    ProductName = g.FirstOrDefault().Product.Name,
                    TotalQuantity = g.Sum(i => i.Quantity)
                })
                .OrderByDescending(x => x.TotalQuantity)
                .Take(5)
                .Select(x => new TopProduct { 
                    productName = x.ProductName,
                    TotalQuantity = x.TotalQuantity
                }).ToListAsync();
            return topProducts;
        }

        public async Task<List<TopCategory>> TopCategories()
        {
            var topcategories = await _context.OrderItems.Include(o => o.Product).ThenInclude(i => i.Category).GroupBy(i => i.Product.CategoryId).Select(g => new
            {
                categoryName = g.FirstOrDefault().Product.Category.Name,
                totalQuantity = g.Sum(i => i.Quantity)
            }).OrderByDescending(x => x.totalQuantity).Take(5).Select(x => new TopCategory {
                categoryName = x.categoryName,
                totalQuantity = x.totalQuantity
            }).ToListAsync();
            return topcategories;
        }
    }
}