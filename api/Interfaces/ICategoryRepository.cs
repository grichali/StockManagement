using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Category;
using api.Models;

namespace api.Interfaces
{
    public interface ICategoryRepository
    {

        Task<Category?> GetCategorybyId(int id);
        Task<List<Category>> GetAllCategories();

        Task<Category> CreateCategory(CreateCategoryDto categoryDto, string imageUrl);

        Task<Category?> UpdateCategory(int id,UpdateCategoryDto categoryDto);

        Task<Category?> UpdateCategoryImage(int id, string key);
        Task<bool> DeleteCategory(int id);
    }
}