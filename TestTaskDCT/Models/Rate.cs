using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskDCT.Models
{
    public class Rate
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public char CurrencySymbol { get; set; }
        public string Type { get; set; }
        public double RateUSD { get; set; }
    }
}
