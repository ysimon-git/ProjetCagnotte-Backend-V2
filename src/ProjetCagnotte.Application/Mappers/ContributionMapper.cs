using ProjetCagnotte.Application.DTOs;
using ProjetCagnotte.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetCagnotte.Application.Mappers
{
    public static class ContributionMapper
    {
        public static Contribution FromDto(CreateContributionDto createContributionDto)
        {
            return new Contribution
            {
                  ProductID=createContributionDto.ProductId,
                  Contributor=createContributionDto.ContributorName,
                  Amount=createContributionDto.Amount,
                  DateContribution=DateTime.UtcNow
            };
        }
    }
}
