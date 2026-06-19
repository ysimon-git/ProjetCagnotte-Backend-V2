using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetCagnotte.Domain.Entities
{
    public class Contribution
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public decimal Amount { get; set; }
        public string Contributor { get; set; }=string.Empty;
        public DateTime DateContribution { get; set; }= DateTime.UtcNow;
    }
}
