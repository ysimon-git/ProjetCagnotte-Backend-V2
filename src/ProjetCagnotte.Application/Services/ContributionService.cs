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

        public ContributionService(IContributionRepository contributionRepository)
        {
            _contributionRepository = contributionRepository;
        }



        public async Task AddAsync(CreateContributionDto dto)
        {
            if(dto.Amount <= 0)
            {
                throw new ArgumentException("Amount should be greater than zero");
            }

            await _contributionRepository.AddAsync(ContributionMapper.FromDto(dto));
        }

        async Task<decimal> IContributionService.GetTotalAmountByProductIdAsync(int productID)
        {
            return await _contributionRepository.GetTotalAmountByProductIdAsync(productID);
        }
    }
}
