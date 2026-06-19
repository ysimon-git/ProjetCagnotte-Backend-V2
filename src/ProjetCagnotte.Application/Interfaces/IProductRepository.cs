using ProjetCagnotte.Domain.Entities;

namespace ProjetCagnotte.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductAsync();
        Task<Product?> GetProductByIdAsync(int id);

    }
}
