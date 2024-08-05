using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Order;
using api.Dtos.Statistics;
using api.Models;

namespace api.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetOrdersAll();

        Task<Order?> CreateOrder(CreateOrderDto orderDto,string userId);

        Task<Order?> GetOrderById(int id);

        Task<List<Order>> GetOrdersByUser(string id);

        Task<Order?> Delete(int id);

        Task<GlobalStatisticsDto> GetStatistics(DateTime startDate, DateTime endDate);
    }
}