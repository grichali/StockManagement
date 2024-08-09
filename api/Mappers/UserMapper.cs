using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Dtos.User;

namespace api.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(this User user){
            return new UserDto{
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                fullName = user.fullName,
            };
        }
    }
}