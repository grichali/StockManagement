using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Product;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepo;

        public ProductController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }


        [HttpPost]
        public async Task<IActionResult> createProduct([FromBody] CreateProductDto productDto)
        {
            var product = await _productRepo.createProduct(productDto);
            return Ok(product.ToProductDto());
        }


        [HttpGet]
        public async Task<IActionResult> getAllProduct(){
            var products = await _productRepo.getAllProducts();
            return Ok(products.Select(x=> x.ToProductDto()));
        }

        [HttpPut("update/")]
        public async Task<IActionResult> updateProduct([FromRoute] int id ,[FromBody] UpdateProductDto updateProductDto)
        {
            var product = await _productRepo.updateProduct(id, updateProductDto);
            return Ok(product.ToProductDto());
        }
    }
} 