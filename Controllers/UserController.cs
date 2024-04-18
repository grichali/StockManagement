using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.User;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
      private readonly IUserRepository _userRepo;

        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpPost("/signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto signUpDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userRepo.Signup(signUpDto);
            return Ok(user);
        }


        [HttpPost("/login")]
        public async Task<IActionResult> LogIn([FromBody] LogInDto logInDto)
        {
            var login = await _userRepo.LogIn(logInDto);
            if (login == false)
            {
                return NotFound();
            }

            return Ok(login);
        }

        [HttpGet("/GetAll")]
        public async Task<IActionResult> GelAll()
        {
            var users =  await _userRepo.GetAll();
            return Ok(users);
        }


        [HttpGet("/GetById/{id}")]
        public async Task<User?> GetById([FromRoute]int id)
        {
            var user = await _userRepo.GetById(id);
            return user;
        }
    }
}