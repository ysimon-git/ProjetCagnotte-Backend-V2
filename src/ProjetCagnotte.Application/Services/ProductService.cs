using ProjetCagnotte.Application.DTOs;
using ProjetCagnotte.Application.Interfaces;
using ProjetCagnotte.Application.Mappers;
using ProjetCagnotte.Domain.Entities;

namespace ProjetCagnotte.Application.Services
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;
        private readonly IContributionRepository _contributionRepository;


        public ProductService(IProductRepository productRepository,IContributionRepository contributionRepository)
        {
            _productRepository = productRepository;
            _contributionRepository = contributionRepository;

        }



        public async Task<IEnumerable<ProductDto>> GetAllProductAsync()
        {
            var products=await _productRepository.GetAllProductAsync();
            var result=new List<ProductDto>();

            foreach (var product in products)
            {
                var fundedAmount=await _contributionRepository.GetTotalAmountByProductIdAsync(product.ID);

                result.Add(ProductMapper.ToDto(product, fundedAmount));
            }

            return result;
        }



        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var product=await _productRepository.GetProductByIdAsync(id);
            
            if (product == null)
                return null;

            var fundedAmount = await _contributionRepository.GetTotalAmountByProductIdAsync(product.ID);

            var result= ProductMapper.ToDto(product,fundedAmount);
                        
            return result;

        }

        public async Task<int> AddProduct(CreateProductDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.ProductName))
                throw new ArgumentException("Product name is mandatory");

            if (dto.Price <= 0)
                throw new ArgumentException("Price must be greater than 0");



            var product=ProductMapper.FromDto(dto);
            return await _productRepository.AddProductAsync(product);

        }

        public async Task<bool> UpdateProduct(int id,UpdateProductDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.ProductName))
                throw new ArgumentException("Product name is mandatory");

            if (dto.Price <= 0)
                throw new ArgumentException("Price must be greater than 0");



            var product = ProductMapper.FromDtoUpdate(dto);
            return await _productRepository.UpdateProductAsync(id, product);

        }

        public async Task<bool> DeleteProduct(int id)
        {
           return await _productRepository.DeleteProductAsync(id);
        }

 
    }
}
