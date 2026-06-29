using ProjetCagnotte.Application.DTOs;
using ProjetCagnotte.Domain.Entities;


namespace ProjetCagnotte.Application.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto ToDto(Product product,decimal fundedAmount)
        {

            return new ProductDto
            {
                ID = product.ID,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                FundedAmount = fundedAmount,
                RemainingAmount = product.Price - fundedAmount,
                IsFullyFunded = fundedAmount >= product.Price
            };
        }


        public static Product FromDto(CreateProductDto dto)
        {
            return new Product
            {
                ProductName = dto.ProductName,
                ProductDescription = dto.ProductDescription,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl,
            };
        }

        public static Product FromDtoUpdate(UpdateProductDto dto)
        {
            return new Product
            {
                ProductName = dto.ProductName,
                ProductDescription = dto.ProductDescription,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl,
            };
        }
    }
}
