using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models ;

namespace api.Dtos.User
{
    public class UserDto
    {
        public string Id { get; set; }

        public string UserName { get; set; } = string.Empty;


        public string Email { get; set; }   = string.Empty;


        public string Token  { get; set; } = string.Empty;

        public string fullName {get; set;} = string.Empty;

    }
}