using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetCagnotte.Domain.Entities
{
    public class Product
    {
        public int ID { get; set; }
        public string ProductName { get; set; }= string.Empty;
        public string ProductDescription { get; set; }= string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }= string.Empty;
    }
}
