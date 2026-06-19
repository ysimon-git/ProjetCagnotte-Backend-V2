using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetCagnotte.Application.DTOs
{
    public class CreateContributionDto
    {
        public int ProductId { get; set; }
        public string ContributorName { get; set; } = string.Empty;
        public decimal Amount { get; set; }

    }
}
