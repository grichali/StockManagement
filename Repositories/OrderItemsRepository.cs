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

        public Task<List<OrderItems>> CreateOrderItems(List<CreateOrderItemDTO> itemDTOs, int orderid)
        {
            throw new NotImplementedException();
        }
    }
}