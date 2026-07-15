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

        public async Task<int> AddProductAsync(Product product)
        {
            await _appDbContext.Products.AddAsync(product);
            await _appDbContext.SaveChangesAsync();

            return product.ID;
        }

        public async Task<bool> UpdateProductAsync(int id, Product product)
        {
            var p=await _appDbContext.Products.FirstOrDefaultAsync(i=>i.ID == id);
            
            if(p==null)
            {
                throw new ArgumentException("Product ID not found");
            }
            else
            {
                p.ProductName = product.ProductName;
                p.ProductDescription = product.ProductDescription;
                p.Price = product.Price;
                //p.ImageUrl = product.ImageUrl;
            }


            var rowsAffected = await _appDbContext.SaveChangesAsync();
            return rowsAffected > 0;

        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var p=await _appDbContext.Products.FirstOrDefaultAsync(i=>i.ID==id);

            if (p==null)
            {
                throw new ArgumentException("Product ID not found");
            }

            _appDbContext.Products.Remove(p);

            var rowsAffected = await _appDbContext.SaveChangesAsync();

            return rowsAffected > 0;

        }
    }
}
