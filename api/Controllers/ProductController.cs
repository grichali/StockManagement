using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Product;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;
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

                imageUrl = await _S3service.UploadImageAsync(imageFile, "products");

                
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error uploading image: {ex.Message}");
            }
            var product = await _productRepo.createProduct(productDto, imageUrl);
            return Ok(product.ToProductDto());
        }



        [HttpGet("GetAll")]
        // [Authorize(Roles ="Admin")]
        public async Task<IActionResult> getAllProduct(){
            List<Product> products = await _productRepo.getAllProducts();
            return Ok(products.Select(x=> x.ToProductDto()));
        }

        [HttpGet("getproductbycategorie/{id}")]
        public async Task<IActionResult> getAllProductsByCategorie([FromRoute] int id)
        {
            List<Product> products = await _productRepo.getAllProductsByCategorie(id);
            List<ProductDto> productDtos = products.Select(product => {
                string ImageUrl = _S3service.GetImageUrl(product.ImageUrl);
                ProductDto productDto = product.ToProductDto();
                productDto.ImageUrl = ImageUrl;
                return productDto;
            }).ToList();
            return Ok(productDtos);        
        }

        [HttpPut("update/")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> updateProduct(int id ,[FromBody] UpdateProductDto updateProductDto)
        {
            var product = await _productRepo.updateProduct(id, updateProductDto);
            return Ok(product.ToProductDto());
        }

        [HttpPut("updateproductimage/{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> updateProductImage([FromRoute] int id , IFormFile imageFile)
        {
            try
            {
            if(imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("Imagefile is required");
            }

            Product? product = await _productRepo.getProductById(id);
            if(product == null)
            {
                return NotFound("Product Not Found");
            }
            await _S3service.DeleteImageAsync(product.ImageUrl);


            string key = await _S3service.UploadImageAsync(imageFile,"categories");

            Product? product1 = await _productRepo.updateProductImage(id, key);

            product1.ImageUrl = _S3service.GetImageUrl(product1.ImageUrl);
            
            return Ok(product1.ToProductDto());

            }catch(Exception e)
            {
                return BadRequest("Error occured during updating category image"+ e);
            }
        }


        [HttpDelete("delete/")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> deleteProduct(int id)
        {
            Product? prod = await _productRepo.getProductById(id);
            if(prod == null)
            {
                return BadRequest("Product Not found");
            }

            await _S3service.DeleteImageAsync(prod.ImageUrl);
            Product? product = await _productRepo.DeleteProduct(id);
            
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