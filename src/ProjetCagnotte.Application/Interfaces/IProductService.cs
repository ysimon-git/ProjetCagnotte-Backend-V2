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
        Task<int> AddProduct(CreateProductDto product);
        Task<Boolean> UpdateProduct(int id,UpdateProductDto product);
        Task<Boolean> DeleteProduct(int id);
    }
}
