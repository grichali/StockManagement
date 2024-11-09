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
        Task<Product> updateProductImage(int id , string imageUrl);
        
        Task<List<Product>> getAllProducts();
        Task<Product?> getProductById(int id);


        Task<Product?> DeleteProduct(int productId);

        Task<List<Product>> getAllProductsByCategorie(int categorieId);
        Task<int> totalProducts();
    }
}