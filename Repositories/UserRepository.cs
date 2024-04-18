using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.User;
using api.Interfaces;
using api.Models;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class UserRepository :IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<User> Signup(SignUpDto userdto)
        {
            var newuser = new User{
                Name = userdto.Name,
                Email = userdto.Email,
                Password = userdto.Password,
                Role  = userdto.Role,
            };

            await _context.User.AddAsync(newuser);
            await _context.SaveChangesAsync();
            return newuser;
        }

        public async Task<bool> LogIn(LogInDto logindto){
            var user = await _context.User.FirstOrDefaultAsync(x => x.Email == logindto.Email);

            if(user != null && VerifyPassword(logindto.Password, user.Password)){
                return true;
            }

            return false;
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public async Task<List<User>> GetAll()
        {
            var users = await _context.User.ToListAsync();
            return users;
        }

        public async Task<User?> GetById(int id){
            var user = await _context.User.FindAsync(id);
            if(user == null)
            {
                return null ;
            }
            return user;
        }
    }
}