using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.User
{
    public class LogInDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}