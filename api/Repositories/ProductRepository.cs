using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Product; 
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
 
namespace api.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Product> createProduct(CreateProductDto productDto, string imageUrl)
        {
            var category = await _context.Category.FindAsync(productDto.CategoryId);
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            var product = new Product{
                Name  = productDto.Name,
                Price = productDto.Price,
                Quantity = productDto.Quantity,
                Category = category,
                ImageUrl = imageUrl
            };

            await _context.Product.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;

        }

        public async Task<Product> updateProduct(int id , UpdateProductDto updateProductDto)
        {

            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                throw new Exception("Product not found");
            }

            
            if(!string.IsNullOrWhiteSpace(updateProductDto.Name))
            {
                product.Name = updateProductDto.Name;
            }

            if(!float.IsNaN(updateProductDto.Price))
            {
                product.Price = updateProductDto.Price;
            }

            if(!float.IsNaN(updateProductDto.Quantity))
            {
                product.Quantity = updateProductDto.Quantity;
            }

            if(updateProductDto.CategoryId != 0)
            {
                var category = await _context.Category.FindAsync(updateProductDto.CategoryId);
                if (category == null)
                {
                    throw new Exception("Category not found");
                }
                product.Category = category;
            }
            if (_context.ChangeTracker.HasChanges())
            {
                await _context.SaveChangesAsync();
            }

            return product;
        }
        public async Task<List<Product>> getAllProducts()
        {
            var products = await _context.Product.Include(c => c.Category).ToListAsync();

            return products;

        }

        public async Task<Product?> DeleteProduct(int productId)
        {
            var product = await _context.Product.Include(p => p.OrderItems).FirstOrDefaultAsync(p => p.Id == productId);
            Console.WriteLine("product to delete is : ");
            Console.WriteLine(product);
            if (product != null)
            {
                foreach (var orderItem in product.OrderItems)
                {
                    _context.OrderItems.Remove(orderItem);
                }

                _context.Product.Remove(product);
                await _context.SaveChangesAsync();
                return product;
            }

            return null;
        }

    }
} 