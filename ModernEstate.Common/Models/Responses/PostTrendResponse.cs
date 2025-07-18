using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernEstate.Common.Models.Responses
{
    public class PostTrendResponse
    {
        public string Date { get; set; } = string.Empty;
        public double RentPercentage { get; set; }
        public double SellPercentage { get; set; }
    }
}
