using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Category;
using api.Dtos.Product;

namespace api.Dtos.Statistics
{
    public class GlobalStatisticsDto
    {
        public List<ProductStatisticsDto> ProductStatistics { get; set; }
        public List<CategoryStatisticsDto> CategoryStatistics { get; set; }

        public decimal TotalProfit { get; set; }
    }
}