using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using api.Dtos.Category;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly S3Service _S3service;
        public CategoryController(ICategoryRepository categoryRepository,IWebHostEnvironment webHostEnvironment, S3Service s3Service)
        {
            _categoryRepo = categoryRepository;
            _webHostEnvironment = webHostEnvironment;
            _S3service = s3Service;
        }

        [HttpPost("Create")]
        // [Authorize(Roles ="Admin")]
        public async Task<IActionResult> CreateCategory([FromForm] CreateCategoryDto categoryDto)
        {
            if (!ModelState.IsValid || categoryDto == null)
            {
                return BadRequest("Invalid category data.");
            }

            string imageUrl;
            try
            {
                IFormFile imageFile = categoryDto.Image;

                if(imageFile == null || imageFile.Length == 0)
                {
                    return BadRequest("Imagefile is required");
                }
                imageUrl = await _S3service.UploadImageAsync(imageFile, "categories");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            var category = await _categoryRepo.CreateCategory(categoryDto, imageUrl);
            return Ok(category.ToCategoryDto());
        }
 
        [HttpGet("getall")]
        // [Authorize(Roles ="Admin, User")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                List<Category> categories = await _categoryRepo.GetAllCategories();
                List<CategoryDto> categoryDtos = categories.Select(category =>
                {
                    string imageUrl = _S3service.GetImageUrl(category.ImageUrl);
                    var categoryDto = category.ToCategoryDto();
                    categoryDto.ImageUrl = imageUrl;
                    return categoryDto;
                }).ToList();

                return Ok(categoryDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        } 
 
        [HttpPut("update/{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDto categoryDto)
        {
            if (!ModelState.IsValid || categoryDto == null)
            {
                return BadRequest("Invalid category data.");
            }

            try
            {    
                var updatedCategory = await _categoryRepo.UpdateCategory(id, categoryDto);
                if (updatedCategory == null)
                {
                    return NotFound("Category not found.");
                }
                return Ok(updatedCategory.ToCategoryDto());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("updateimage/{id}")]
        // [Authorize(Roles ="Admin")]
        public async Task<IActionResult> UpdateCategoryImage([FromRoute]int id, IFormFile imageFile)
        {
            try
            {
            if(imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("Imagefile is required");
            }

            Category? category = await _categoryRepo.GetCategorybyId(id);
            if(category == null)
            {
                return NotFound("Category Not Found");
            }
            await _S3service.DeleteImageAsync(category.ImageUrl);
            if(imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("Imagefile is required");
            }

            string key = await _S3service.UploadImageAsync(imageFile,"categories");

            Category category1 = await _categoryRepo.UpdateCategoryImage(id, key);
            
            return Ok(category1.ToCategoryDto());
            }catch(Exception e)
            {
                return BadRequest("Error occured during updating category image"+ e);
            }
        }  

        [HttpDelete("delete/{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteCategory([FromRoute]int id )
        {
            try
            {
                Category? X = await _categoryRepo.GetCategorybyId(id);
                if(X == null)
                {
                    return NotFound("Category Not Found");
                }
                await _S3service.DeleteImageAsync(X.ImageUrl);
                bool category = await _categoryRepo.DeleteCategory(id);
                if(category == true)
                {
                    return Ok("Category has been deleted");
                }

                return NotFound("Category Not Found");
            }
            catch(Exception e)
            {
                return BadRequest("Error occured during deleting category"+ e);
            };

        }
    }
}
