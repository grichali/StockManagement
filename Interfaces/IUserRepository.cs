using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.User;
using api.Models;

namespace api.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Signup(SignUpDto userdto);

        Task<bool> LogIn(LogInDto logindto);


        Task<List<User>> GetAll();

        Task<User?> GetById(int id);

        Task<User?> Delete(int id, int userid);
    }
}