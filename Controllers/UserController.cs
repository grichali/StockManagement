using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.User; 
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Repositories;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IEmailVerificationService _emailVerificationService;

        private readonly ApplicationDbContext _context;

        public UserController(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager
        ,IEmailVerificationService emailVerificationService,ApplicationDbContext context)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _emailVerificationService = emailVerificationService;
            _context = context;
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
    
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("Email is required.");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound("Email incorrect");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            await _emailVerificationService.SendEmailAsync(user.Email, "Reset Password", $"Your token is : <b>{token}</b>");

            return Ok("Password reset token has been sent to your email.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            if (string.IsNullOrWhiteSpace(resetPasswordDto.email) || string.IsNullOrWhiteSpace(resetPasswordDto.token) || string.IsNullOrWhiteSpace(resetPasswordDto.newPassword))
            {
                return BadRequest("Email, token, and new password are required.");
            }

            var user = await _userManager.FindByEmailAsync(resetPasswordDto.email);
            if (user == null)
            {
                return BadRequest("Invalid request.");
            }

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.token, resetPasswordDto.newPassword);
            if (result.Succeeded)
            {
                return Ok("Password has been reset !");
            }

            return BadRequest("Invalid Or Expired Token");
        }
        [HttpDelete("delete")]
        [Authorize]
       public async Task<IActionResult> DeleteUser()
        {
            string username = User.GetUsername();

            User? user = await _userManager.Users
                .Include(u => u.Orders)
                    .ThenInclude(o => o.OrderItems)
                .FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null)
            {
                return BadRequest("User Not Found");
            }

            foreach (var order in user.Orders)
            {
                foreach (var orderItem in order.OrderItems)
                {
                    _context.OrderItems.Remove(orderItem);
                }
                _context.Order.Remove(order);
            }

            await _context.SaveChangesAsync();

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok("User Deleted Successfully");
            }
            else
            {
                return StatusCode(500, "Error deleting user");
            }
        }

    }
}