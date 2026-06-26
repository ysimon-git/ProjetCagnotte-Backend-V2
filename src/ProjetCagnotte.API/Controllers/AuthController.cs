using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjetCagnotte.Application.DTOs;
using ProjetCagnotte.Application.Interfaces;
using ProjetCagnotte.Domain.Entities;

namespace ProjetCagnotte.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        //test of testbranch
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {

            await _authService.RegisterAsync(registerDto);

            return Ok(new {message="User registered"});
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {

            await _authService.LoginAsync(loginDto);

            return Ok(new { message = "Connection done" });


        }



    }
}
