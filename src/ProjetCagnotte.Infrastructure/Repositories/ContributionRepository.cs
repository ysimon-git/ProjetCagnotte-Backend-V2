using Microsoft.EntityFrameworkCore;
using ProjetCagnotte.Application.Interfaces;
using ProjetCagnotte.Domain.Entities;
using ProjetCagnotte.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetCagnotte.Infrastructure.Repositories
{
    public class ContributionRepository : IContributionRepository
    {
        private readonly AppDbContext _appDbContext;


        public ContributionRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }



        public async Task AddAsync(Contribution contribution)
        {
            _appDbContext.Contributions.Add(contribution);
            await _appDbContext.SaveChangesAsync();
        }



        public async Task<decimal> GetTotalAmountByProductIdAsync(int productID)
        {
            decimal totalAmount = 0;
            totalAmount = await _appDbContext.Contributions
                .Where(p => p.ProductID == productID)
                .SumAsync(c => c.Amount);
      
            return totalAmount;
        }
    }
}
