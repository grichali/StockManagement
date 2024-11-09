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

        private readonly S3Service _S3service;
        
        public ProductController(IProductRepository productRepo,IWebHostEnvironment webHostEnvironment, S3Service s3Service)
        {
            _productRepo = productRepo;
            _webHostEnvironment = webHostEnvironment;
            _S3service = s3Service;

        }


        [HttpPost("Create")]
        // [Authorize(Roles ="Admin")]
        public async Task<IActionResult> createProduct([FromForm] CreateProductDto productDto)
        {
            // Upload the image to S3
            string imageUrl;
            try
            {
                IFormFile imageFile = productDto.Image;
                if (imageFile == null || imageFile.Length == 0)
                {
                    return BadRequest("Image file is required");
                }

                string key = $"products/{Guid.NewGuid()}_{imageFile.FileName}";
                using var fileStream = imageFile.OpenReadStream();
                await _S3service.UploadImageAsync(key, fileStream, imageFile.ContentType);

                imageUrl = _S3service.GetImageUrl(key);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error uploading image: {ex.Message}");
            }

            // Save product data
            var product = await _productRepo.createProduct(productDto, imageUrl);
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
        [HttpGet("/totalproducts")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> getTotalProducts(){
            int totalproducts = await _productRepo.totalProducts();
            return Ok(totalproducts);
        }

    }
} 