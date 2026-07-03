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
        private readonly IJwtTokenGenerator _jwtTokenGenerator;


        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }


        public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
        {

            var user=await _userManager.FindByEmailAsync(dto.Email);
            
            if(user==null)
            {
                throw new UnauthorizedAccessException("Invalid email");
            }

            var isValidPassword=await _userManager.CheckPasswordAsync(user,dto.Password);

            if(!isValidPassword)
            {
                throw new UnauthorizedAccessException("Invalid password");
            }


            var token = _jwtTokenGenerator.GenerateToken(user);

            return new LoginResponseDto
            {
                Email = dto.Email,
                Token = token
            };
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
