using backend.Context;
using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class CategoryServices
    {
        private readonly butikContext _butikContext;

        public CategoryServices(butikContext butikContext)
        {
            _butikContext = butikContext;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            var categories = await _butikContext.Categories.ToListAsync();
            return categories?.Any() == true ? categories : null;
        }

        public async Task<Category> GetCategoryByIdAsync (int id)
        {
            var category = await _butikContext.Categories.FindAsync(id);
            if (category == null)
            {
                return null;
            }

            return category;

            


        }   

        public async Task<Category> GetCategoryByNameAsync(string CategoryName)
        {
            var category = await _butikContext.Categories.FirstOrDefaultAsync(c => c.Name == CategoryName);
            if (category == null)
            {
                return null;

            }

            var products = await _butikContext.Products
                .Where(p => p.CategoryId == category.Id)
                .ToListAsync();

            category.Products = products;
            return category;


        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            await _butikContext.Categories.AddAsync(category);
            await _butikContext.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _butikContext.Categories.FindAsync(id);
            if (category == null)
            {
                return false;
            }
            _butikContext.Categories.Remove(category);
            await _butikContext.SaveChangesAsync();
            return true;
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            var updateCategory = await _butikContext.Categories.FindAsync(category.Id);
            if (updateCategory == null)
            {
                return null;
            }
            updateCategory.Name = category.Name;
            await _butikContext.SaveChangesAsync();
            return updateCategory;
 
       }

    }
}