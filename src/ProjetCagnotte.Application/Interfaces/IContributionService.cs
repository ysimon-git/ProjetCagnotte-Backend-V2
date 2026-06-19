using ProjetCagnotte.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetCagnotte.Application.Interfaces
{
    public interface IContributionService
    {
        Task AddAsync(CreateContributionDto dto);
        Task<decimal> GetTotalAmountByProductIdAsync(int productID);
    }
}
