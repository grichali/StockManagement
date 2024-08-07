using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Category
{
    public class CategoryDto
    {
        public int id { get; set; }

        public string Name { get; set; }    = string.Empty;

        public string  Description { get; set; }    = string.Empty; 

        public string ImageUrl { get; set; }  = string.Empty;  
    }
}