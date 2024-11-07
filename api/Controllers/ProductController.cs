using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Product;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        
        public ProductController(IProductRepository productRepo,IWebHostEnvironment webHostEnvironment)
        {
            _productRepo = productRepo;
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpPost("Create")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> createProduct([FromForm] CreateProductDto productDto)
        {
            string imageUrl = await productDto.Image.UploadProduit(_webHostEnvironment);
            var product = await _productRepo.createProduct(productDto,imageUrl);
            return Ok(product.ToProductDto());
        }


        [HttpGet("GetAll")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> getAllProduct(){
            var products = await _productRepo.getAllProducts();
            return Ok(products.Select(x=> x.ToProductDto()));
        }

        [HttpGet("getproductbycategorie/{id}")]
        public async Task<IActionResult> getAllProductsByCategorie([FromRoute] int id)
        {
            var products = await _productRepo.getAllProductsByCategorie(id);
            return Ok(products.Select(x=> x.ToProductDto()));        
        }

        [HttpPut("update/")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> updateProduct(int id ,[FromBody] UpdateProductDto updateProductDto)
        {
            var product = await _productRepo.updateProduct(id, updateProductDto);
            return Ok(product.ToProductDto());
        }

        [HttpDelete("delete/")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> deleteProduct(int id)
        {
            var product = await _productRepo.DeleteProduct(id);

            if(product == null)
            {
                return BadRequest("Product Not found");
            }

            return Ok("product has been deleted successfully");
        }

    }
} 