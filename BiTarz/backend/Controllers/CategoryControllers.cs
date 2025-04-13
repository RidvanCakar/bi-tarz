using Microsoft.AspNetCore.Mvc;
using backend.Services;
using backend.Entities;

namespace backend.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CategoryControllers : ControllerBase
    {
        private readonly CategoryServices _categoryServices;
        public CategoryControllers(CategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categoris = await _categoryServices.GetCategoriesAsync();
            if (categoris == null)
            {
                return NotFound("Categori Bulunamadı");
            }
            return Ok(categoris);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryServices.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound("Kategori Bulunamadı");
            }
            return Ok(category);
        }

        [HttpGet("{CategoryName}")]
        public async Task<IActionResult> GetCategoryById(string CategoryName)
        {
            var category = await _categoryServices.GetCategoryByNameAsync(CategoryName);
            if (category == null)
            {
                return NotFound("Categori Bulunamadı");
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest("Geçersiz kategori verisi");
            }
            var createdCategory = await _categoryServices.CreateCategoryAsync(category);
            return Ok(createdCategory);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryServices.DeleteCategoryAsync(id);
            if (!result)
            {
                return NotFound("Categori Bulunamadı");
            }
            return Ok("Categori Silindi");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest("Geçersiz kategori verisi");
            }

            var updatedCategory = await _categoryServices.UpdateCategoryAsync(category);
            if (updatedCategory == null)
            {
                return NotFound("Categori Bulunamadı");
            }
            return Ok(updatedCategory);
        }

    }
}