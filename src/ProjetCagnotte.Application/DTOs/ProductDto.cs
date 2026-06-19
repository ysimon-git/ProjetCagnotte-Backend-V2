using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetCagnotte.Application.DTOs
{
    public class ProductDto
    {

        public int ID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public decimal FundedAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public bool IsFullyFunded { get; set; }


    }
}
