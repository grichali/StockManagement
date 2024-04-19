using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; }   = string.Empty;

        public string Password { get; set; }    = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty;

        public List<Order> Orders { get; set; } = new List<Order>();

    }
} 