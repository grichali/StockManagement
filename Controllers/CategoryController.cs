using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Dtos.Category;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepo = categoryRepository;
        }

        [HttpPost("Create")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto categoryDto)
        {
            if (!ModelState.IsValid || categoryDto == null)
            {
                return BadRequest("Invalid category data.");
            }

            try
            {
                var createdCategory = await _categoryRepo.CreateCategory(categoryDto);
                return Ok(createdCategory.ToCategoryDto());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
 
        [HttpGet("getall")]
        [Authorize(Roles ="Admin, User")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _categoryRepo.GetAllCategories();
                return Ok(categories.Select(x => x.ToCategoryDto()));
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

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id )
        {
            bool category = await _categoryRepo.DeleteCategory(id);
            if(category == true)
            {
                return Ok("Category has been deleted");
            }

            return NotFound("Category Not Found");

        }
    }
}
