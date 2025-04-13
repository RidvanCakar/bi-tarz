using backend.Context;
using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class ProductServices
    {
        private readonly butikContext _butikContext;
        public ProductServices(butikContext butikContext)
        {
            _butikContext = butikContext;
        }

        public async Task<List<ProductDto>> GetProductsAsync()
        {
            var Products = await _butikContext.Products
            .Include(p => p.Category)
            .Include(p => p.User)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Username = p.User.Name,
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                Stock = p.Stock,
                CategoryName = p.Category.Name
            }).ToListAsync();

            return Products;

        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _butikContext.Products
            .Include(p => p.Category)
            .Include(p => p.User)
            .Where(p => p.Id == id)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Username = p.User.Name,
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                Stock = p.Stock,
                CategoryName = p.Category.Name
            }).FirstOrDefaultAsync();


            return product;
        }

        public async Task<List<Product>> GetProductsByUserIdAsync(int userId)
        {
            var products = await _butikContext.Products
            .Where(p => p.UserId == userId)
            .ToListAsync();
            return products;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            await _butikContext.Products.AddAsync(product);
            await _butikContext.SaveChangesAsync();
            return product;
        }


        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _butikContext.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }

            _butikContext.Products.Remove(product);
            await _butikContext.SaveChangesAsync();
            return true;

        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            var updateProduct = await _butikContext.Products.FindAsync(product.Id);
            if (updateProduct == null)
            {
                return null;
            }
            
            updateProduct.Name = product.Name;
            updateProduct.Price = product.Price;
            updateProduct.Stock = product.Stock;
            updateProduct.ImageUrl = product.ImageUrl;
            updateProduct.CategoryId = product.CategoryId;

            _butikContext.Products.Update(updateProduct);
            await _butikContext.SaveChangesAsync();
            return updateProduct;
        }


    }
}