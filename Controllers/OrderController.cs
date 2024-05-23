using Microsoft.AspNetCore.Mvc;
using api.Dtos.Order;
using api.Interfaces;
using api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using api.Extensions;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepo;
        private readonly UserManager<User> _userManager;
        public OrderController(IOrderRepository orderRepository, UserManager<User> userManager)
        {
            _orderRepo = orderRepository;
            _userManager = userManager;
        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderRepo.GetOrdersAll();
            return Ok(orders.Select( e => e.ToOrderDto()));
        }

        [HttpGet("getbyid/{id}")]
        [Authorize]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderRepo.GetOrderById(id);
            if (order == null)
            {
                return NotFound($"Order with ID {id} not found");
            }
            return Ok(order.ToOrderDto());
        }

        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto orderDto)
        {
            string username = User.GetUsername();
            User user = await _userManager.FindByNameAsync(username);
            
            orderDto.UserId=user.Id;
            return Ok(orderDto);
            // if (!ModelState.IsValid)
            // {
            //     return BadRequest(ModelState);
            // }

            // var createdOrder = await _orderRepo.CreateOrder(orderDto);
            // if (createdOrder == null)
            // {
            //     return BadRequest("Failed to create order");
            // }

            // return Ok(createdOrder.ToOrderDto());
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var deletedOrder = await _orderRepo.Delete(id);
            if (deletedOrder == null)
            {
                return NotFound($"Order with ID {id} not found");
            }

            return Ok(deletedOrder);
        }
    }
}
 