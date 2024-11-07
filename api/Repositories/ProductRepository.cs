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

            product.Name = updateProductDto.Name;
            
            product.Price = updateProductDto.Price;

            product.Quantity = updateProductDto.Quantity;

            await _context.SaveChangesAsync();

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

        public async Task<List<Product>> getAllProductsByCategorie(int categorieId)
        {
            var products = await _context.Product.Where(x => x.CategoryId == categorieId && x.Quantity >= 1).ToListAsync();
            return products;
        }
    }
} 