using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjetCagnotte.Application.Interfaces;
using ProjetCagnotte.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjetCagnotte.Infrastructure.Security
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {

        private readonly IConfiguration _configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }




        public string GenerateToken(ApplicationUser user)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email ?? string.Empty),
                new Claim(ClaimTypes.Name,user.UserName ??  string.Empty)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials);

            
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
