using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace api.Models
{
    public class User : IdentityUser
    {

        public string firstname {get; set;} = string.Empty;        
        public string lastname {get; set;} = string.Empty;        
        public List<Order> Orders { get; set; } = new List<Order>();

    }
} 