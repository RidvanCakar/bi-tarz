using backend.Entities;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductControllers : ControllerBase
    {
        private readonly ProductServices _productServices;

        public ProductControllers(ProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllProducts(){
            var products= await _productServices.GetProductsAsync();
            if(products == null){
                return NotFound("Ürün Bulunamadı");
            }
            return Ok(products);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetProductById(int id){
            var product = await _productServices.GetProductByIdAsync(id);
            if(product == null){
                return NotFound("Ürün Bulunamadı");
            }

            return Ok(product);
        }

        [HttpGet("{UserId}")]

        public async Task<IActionResult> GetProductsByUserId(int UserId){
            var products = await _productServices.GetProductsByUserIdAsync(UserId);
            if(products == null){
                return NotFound("Ürün Bulunamadı");
            }
            return Ok(products);
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product) 
        {
            if(product==null){
                return BadRequest("Geçersiz ürün verisi");
            }

            var createdProduct= await _productServices.CreateProductAsync(product);
            return Ok(createdProduct);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id){
            var result = await _productServices.DeleteProductAsync(id);
            if(!result){
                return NotFound("Ürün Bulunamadı");
            }
            return Ok("Ürün Silindi");
        }

        [HttpPut("{id}")]
        public async Task <IActionResult> UpdateProduct([FromBody] Product product){
            if(product == null){
                return BadRequest("Geçersiz ürün ");
            }

            var updatedProduct = await _productServices.UpdateProductAsync(product);
    
            return Ok(updatedProduct);
        }


    }

}