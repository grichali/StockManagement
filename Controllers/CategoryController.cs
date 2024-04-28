using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Dtos.Category;
using api.Interfaces;
using api.Mappers;
using api.Models;
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
            _categoryRepo = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        [HttpPost("Create")]
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

        [HttpPut("{id}")]
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
    }
}
