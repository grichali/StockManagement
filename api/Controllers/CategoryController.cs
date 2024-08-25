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
        public CategoryController(ICategoryRepository categoryRepository,IWebHostEnvironment webHostEnvironment)
        {
            _categoryRepo = categoryRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost("Create")]
        // [Authorize(Roles ="Admin")]
        public async Task<IActionResult> CreateCategory([FromForm] CreateCategoryDto categoryDto)
        {
            if (!ModelState.IsValid || categoryDto == null)
            {
                return BadRequest("Invalid category data.");
            }

            try
            {
                string imageUrl = await categoryDto.Image.UploadCategory(_webHostEnvironment);
                var createdCategory = await _categoryRepo.CreateCategory(categoryDto,imageUrl);
                return Ok(createdCategory.ToCategoryDto());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
 
        [HttpGet("getall")]
        // [Authorize(Roles ="Admin, User")]
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
        // [Authorize(Roles ="Admin")]
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
         // [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteCategory(int id )
        {
            bool category = await _categoryRepo.DeleteCategory(id);
            if(category == true)
            {
                return Ok("Category has been deleted");
            }

            return NotFound("Category Not Found");

        }

        [HttpPost("upload-excel")]
         // [Authorize(Roles ="Admin")]
        public async Task<IActionResult> UploadAndExtractData(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file provided.");

            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    using (var workbook = new XLWorkbook(stream))
                    {
                        var worksheet = workbook.Worksheets.First();
                        var data = ExtractColumns(worksheet);

                        return Ok(JsonSerializer.Serialize(data));
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private List<Dictionary<string, string>> ExtractColumns(IXLWorksheet worksheet)
        {
            var extractedData = new List<Dictionary<string, string>>();

            // Assuming headers are in the first row
            var headers = worksheet.Row(1).Cells().Select(c => c.Value.ToString()).ToList();

            // Map of your desired column names to actual header names in the file
            var columnMapping = new Dictionary<string, string>
            {
                { "nom composent", "Product name" },  // or "Search name (EN-US)" depending on your needs
                { "serialnumber", "Serial number" },
                { "articlenumber", "Item base id" },
                { "etat composent", "QS-Check status" }  // or any other status-related column
            };

            foreach (var row in worksheet.RowsUsed().Skip(1))
            {
                var rowData = new Dictionary<string, string>();

                foreach (var (desiredName, actualHeader) in columnMapping)
                {
                    var header = headers.FirstOrDefault(h => h.Equals(actualHeader, StringComparison.OrdinalIgnoreCase));

                    if (header != null)
                    {
                        var cellValue = row.Cell(headers.IndexOf(header) + 1).GetValue<string>();
                        rowData[desiredName] = cellValue;
                    }
                }

                if (rowData.Any())
                    extractedData.Add(rowData);
            }

            return extractedData;
        }
    }
}
