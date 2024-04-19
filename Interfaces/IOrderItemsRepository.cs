using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.OrderItems;
using api.Models;

namespace api.Interfaces
{
    public interface IOrderItemsRepository
    {


        Task<List<OrderItems>> CreateOrderItems(List<CreateOrderItemDTO> itemDTOs, int orderid);
    }
}