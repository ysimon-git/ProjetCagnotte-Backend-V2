using ProjetCagnotte.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetCagnotte.Application.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterDto dto);
        Task LoginAsync(LoginDto dto);
    }
}
