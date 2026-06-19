using ProjetCagnotte.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetCagnotte.Application.Interfaces
{
    public interface IContributionRepository
    {
        Task AddAsync(Contribution contribution);
        Task<decimal> GetTotalAmountByProductIdAsync(int productID);
    }
}
