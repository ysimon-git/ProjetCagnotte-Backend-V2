using ProjetCagnotte.Application.DTOs;
using ProjetCagnotte.Application.Interfaces;
using ProjetCagnotte.Application.Mappers;

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
    }
}
