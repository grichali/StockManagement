using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using api.Dtos.User;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpPost("/signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto signUpDto)
        {
            var user = await _userRepo.Signup(signUpDto);
            return Ok(user.ToUseDto());
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
            return Ok(users.Select(s => s.ToUseDto()));
        }


        [HttpGet("/GetById/{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var user = await _userRepo.GetById(id);
            if (user == null)
            {
                return NotFound() ;
            }
            return Ok(user.ToUseDto());
        }


        [HttpDelete("{userid}/{id}")]
        public async Task<IActionResult> DeleteById( [FromRoute] int userid, [FromRoute]int id){
            var user = await _userRepo.Delete(id, userid);
            if(user == null)
            {
                return BadRequest();
            }

            return Ok(user.ToUseDto());

        }
    }
}