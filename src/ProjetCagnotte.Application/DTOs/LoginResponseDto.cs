using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetCagnotte.Application.DTOs
{
    public class LoginResponseDto
    {
        public string Email { get; set; }=string.Empty;
        public string Token { get; set; }= string.Empty;

    }
}
