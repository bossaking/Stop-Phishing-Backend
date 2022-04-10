using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stop_Phishing.DAL.Interfaces.Services;
using Stop_Phishing.DTO.Auth;
using Stop_Phishing.Models;
using Stop_Phishing.Services;

namespace Stop_Phishing.Controllers
{
    [ApiController]
    [Route("auth/")]
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;

        public AuthController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest user)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if (existingUser != null)
                {
                    return BadRequest();
                }

                var newUser = new User() { Email = user.Email, UserName = user.Username};
                var isCreated = await _userManager.CreateAsync(newUser, user.Password);

                if (isCreated.Succeeded)
                {
                    return Ok();
                }

                return BadRequest();

            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest user)
        {
            var existingUser = await _userManager.FindByEmailAsync(user.Email);

            if (existingUser == null)
            {
                return BadRequest();
            }

            if (await _userManager.CheckPasswordAsync(existingUser, user.Password))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}