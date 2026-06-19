using Microsoft.EntityFrameworkCore;
using ProjetCagnotte.Application.Interfaces;
using ProjetCagnotte.Domain.Entities;
using ProjetCagnotte.Infrastructure.Data;


namespace ProjetCagnotte.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly AppDbContext _appDbContext;


        public ProductRepository(AppDbContext appDbContext) { 
               _appDbContext = appDbContext;       
        }



        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            var products = await _appDbContext.Products.ToListAsync();
            return products;
        }


        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var product=await _appDbContext.Products.FirstOrDefaultAsync(p=>p.ID == id);
            return product;
        }
    }
}
