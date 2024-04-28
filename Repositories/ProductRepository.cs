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
        public async Task<Product?> createProduct(CreateProductDto productDto)
        {
            var product = new Product{
                Name  = productDto.Name,
                Price = productDto.Price,
                Quantity = productDto.Quantity,
                CategoryId = productDto.CategoryId,
            };

            await _context.Product.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;

        }

        public async Task<List<Product>> getAllProducts()
        {
            var products = await _context.Product.Include(c => c.Category).ToListAsync();

            return products;

        }
    }
} 