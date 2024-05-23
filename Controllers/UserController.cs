using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using api.Dtos.User;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<User> _signInManager;
        public UserController(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("user/signup")]
        public async Task<IActionResult> UserSignUp([FromBody] SignUpDto signUpDto)
        {
                try{
                    if(!ModelState.IsValid){
                        return BadRequest(ModelState);
                    }
                    if(await signUpDto.Email.IfEmailExists(_userManager)){
                        return BadRequest("Email already exists");
                    }
                    var user = new User{
                        UserName = signUpDto.UserName,
                        Email = signUpDto.Email
                    };

                    var createduser = await _userManager.CreateAsync(user, signUpDto.Password);
                    if(createduser.Succeeded)
                    {
                        var roleResult = await _userManager.AddToRoleAsync(user, "User");
                        if(roleResult.Succeeded)
                        {
                            var roles = await _userManager.GetRolesAsync(user);
                            return Ok(new UserDto{
                                Id = user.Id,
                                UserName = user.UserName,
                                Email = user.Email,
                                Token = _tokenService.CreateToken(user, roles)
                            });
                        }else{
                            return StatusCode(500, "Error while assigning role");
                        }
                    }else{
                            return StatusCode(500, createduser.Errors);
                        }
                }catch(Exception e )
                {
                    return StatusCode(500, "Error");
                }
        }

        [HttpPost("admin/signup")]
        public async Task<IActionResult> AdminSignUp([FromBody] SignUpDto signUpDto)
        {
                try{
                    if(!ModelState.IsValid){
                        return BadRequest(ModelState);
                    }
                    if(await signUpDto.Email.IfEmailExists(_userManager)){
                        return BadRequest("Email already exists");
                    }
                    var user = new User{
                        UserName = signUpDto.UserName,
                        Email = signUpDto.Email
                    };

                    var createduser = await _userManager.CreateAsync(user, signUpDto.Password);
                    if(createduser.Succeeded)
                    {
                        var roleResult = await _userManager.AddToRoleAsync(user, "Admin");
                        if(roleResult.Succeeded)
                        {
                            var roles = await _userManager.GetRolesAsync(user);
                            return Ok(new UserDto{
                                Id = user.Id,
                                UserName = user.UserName,
                                Email = user.Email,
                                Token = _tokenService.CreateToken(user, roles)
                            });
                        }else{
                            return StatusCode(500, "Error while assigning role");
                        }
                    }else{
                            return StatusCode(500, createduser.Errors);
                        }
                }catch(Exception e )
                {
                    return StatusCode(500, "Error");
                }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LogIn([FromBody] LogInDto logInDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == logInDto.UserName);
            if(user == null)
            {
                return Unauthorized("Incorrect password or username");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, logInDto.Password, false);

            if(!result.Succeeded)
            {
                return Unauthorized("Incorrect password or username");
            }

            var roles = await _userManager.GetRolesAsync(user);
            return Ok(
                new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user, roles)    
                }
            );
        }
       
    }
}