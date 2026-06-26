using Microsoft.AspNetCore.Identity;
using ProjetCagnotte.Application.DTOs;
using ProjetCagnotte.Application.Interfaces;
using ProjetCagnotte.Domain.Entities;


namespace ProjetCagnotte.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public AuthService(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task LoginAsync(LoginDto dto)
        {
            var result=await _signInManager.PasswordSignInAsync(dto.Email, dto.Password,false,false);

            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException("invalid email or password");
            }
        }


        public async Task RegisterAsync(RegisterDto dto)
        {
            if(dto.Password != dto.ConfirmPassword)
            {
                throw new ArgumentException("passwords do not match");
            }

            var user = new ApplicationUser
            { 
                Email=dto.Email,
                UserName=dto.Email
            };   
            
            var result=await _userManager.CreateAsync(user,dto.Password);   

            if(!result.Succeeded)
            {
                throw new ArgumentException(result.Errors.First().Description);
            }
        }
    }
}
