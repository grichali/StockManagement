using api.Data;
using api.Dtos.Category;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateCategory(CreateCategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                throw new ArgumentNullException(nameof(categoryDto), "Category DTO cannot be null.");
            }


            var category = new Category{
                Name = categoryDto.Name,
                Description = categoryDto.Description,
            };

            await _context.Category.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            var categories = await _context.Category.ToListAsync();
            return categories;        
        }

        public async Task<Category?> UpdateCategory(int id, UpdateCategoryDto categoryDto)
        {
            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                throw new Exception("Category not found");
            }

            if (!string.IsNullOrWhiteSpace(categoryDto.Name))
            {
                category.Name = categoryDto.Name;
            }

            if (!string.IsNullOrWhiteSpace(categoryDto.Description))
            {
                category.Description = categoryDto.Description;
            }

            if (_context.ChangeTracker.HasChanges())
            {
                await _context.SaveChangesAsync();
            }

            return category;
        }

       public async Task<bool> DeleteCategory(int id)
        {
            Category? category = await _context.Category
                .Include(c => c.Products)
                    .ThenInclude(p => p.OrderItems)
                .FirstOrDefaultAsync(c => c.id == id);

            if (category != null)
            {
                foreach (var product in category.Products)
                {
                    foreach (var orderItem in product.OrderItems)
                    {
                        
                        _context.OrderItems.Remove(orderItem);
                    }
                    
                    _context.Product.Remove(product);
                }

                
                _context.Category.Remove(category);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

    }
}