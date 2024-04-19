using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dtos.User
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;


        public string Email { get; set; }   = string.Empty;


        public string Role { get; set; } = string.Empty;

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}