using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.OrderItems;

namespace api.Dtos.Order
{
    public class TopProduct
    {
        public string productName { set; get; }
        public int TotalQuantity { set; get; }

    }
}