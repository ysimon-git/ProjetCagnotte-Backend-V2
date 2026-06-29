using ProjetCagnotte.Application.DTOs;
using ProjetCagnotte.Domain.Entities;

namespace ProjetCagnotte.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<int> AddProductAsync(Product product);
        Task<Boolean> UpdateProductAsync(int id,Product product);
        Task<Boolean> DeleteProductAsync(int id);

    }
}
