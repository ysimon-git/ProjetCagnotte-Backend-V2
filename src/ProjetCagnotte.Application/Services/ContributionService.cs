using ProjetCagnotte.Application.DTOs;
using ProjetCagnotte.Application.Interfaces;
using ProjetCagnotte.Application.Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetCagnotte.Application.Services
{
    public class ContributionService : IContributionService
    {
        private readonly IContributionRepository _contributionRepository;
        private readonly IProductRepository _productRepository;

        public ContributionService(IContributionRepository contributionRepository, IProductRepository productRepository)
        {
            _contributionRepository = contributionRepository;
            _productRepository = productRepository;
        }



        public async Task AddAsync(CreateContributionDto dto)
        {
            if(dto.Amount <= 0)
            {
                throw new ArgumentException("Amount should be greater than zero");
            }
            if (string.IsNullOrWhiteSpace(dto.ContributorName))
            {
                throw new ArgumentException("Contributor Name is missing");
            }

            var product =await _productRepository.GetProductByIdAsync(dto.ProductId);

            if (product == null)
                throw new ArgumentException("Product not found");

            var totalContributions = await _contributionRepository.GetTotalAmountByProductIdAsync(dto.ProductId);

            var remainingAmount = product.Price - totalContributions;

            if (dto.Amount > remainingAmount)
                throw new ArgumentException("Contribution cannot exceed remaining amount");

            var contribution =ContributionMapper.FromDto(dto);

            await _contributionRepository.AddAsync(contribution);
        }



        async Task<decimal> IContributionService.GetTotalAmountByProductIdAsync(int productID)
        {
            return await _contributionRepository.GetTotalAmountByProductIdAsync(productID);
        }
    }
}
