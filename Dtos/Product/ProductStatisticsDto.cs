using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Product
{
    public class ProductStatisticsDto
    {
        public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int QuantitySold { get; set; }
    public decimal TotalProfit { get; set; }
    }
}
