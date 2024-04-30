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
        Task<Product> createProduct(CreateProductDto productDto);

        Task<Product> updateProduct(int id , UpdateProductDto updateProductDto);
        
         Task<List<Product>> getAllProducts();
    }
}