using ProjetCagnotte.Application.DTOs;
using ProjetCagnotte.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetCagnotte.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductAsync();
        Task<ProductDto?> GetProductByIdAsync(int id);
    }
}
