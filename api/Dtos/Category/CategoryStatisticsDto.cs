using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Category
{
    public class CategoryStatisticsDto
    {
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public decimal TotalProfit { get; set; }
    }
}
