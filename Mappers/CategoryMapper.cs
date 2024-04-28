using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Category;
using api.Models;

namespace api.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDto ToCategoryDto(this Category category){
            return new CategoryDto{
                id = category.id,
                Name = category.Name,
                Description = category.Description,
            };
        }
    }
}