using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Product;
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

        
        public ProductController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }
       private async Task<string> UploadImage(IFormFile image)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "images");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";

            string filePath = Path.Combine(folderPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return $"/assets/images/{fileName}"; 
        } 

        [HttpPost("Create")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> createProduct([FromForm] CreateProductDto productDto)
        {
            string imageUrl = await UploadImage(productDto.Image);
            var product = await _productRepo.createProduct(productDto,imageUrl);
            return Ok(product.ToProductDto());
        }


        [HttpGet("GetAll")]
        [Authorize(Roles ="Admin, User")]
        public async Task<IActionResult> getAllProduct(){
            var products = await _productRepo.getAllProducts();
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