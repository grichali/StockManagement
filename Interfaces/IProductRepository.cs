using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Product;
using api.Models;

namespace api.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> createProduct(CreateProductDto productDto, string imageUrl);  

        Task<Product> updateProduct(int id , UpdateProductDto updateProductDto);
        
         Task<List<Product>> getAllProducts();
         Task<Product?> DeleteProduct(int productId);
    }
}