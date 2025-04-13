using backend.Entities;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ShoppingCardController : ControllerBase
    {

        private readonly ShoppingCardServices _shoppingCardServices;
        public ShoppingCardController(ShoppingCardServices shoppingCardServices)
        {
            _shoppingCardServices = shoppingCardServices;
        }


        [HttpGet]
        
        public async Task<IActionResult> GetAllShoppingCards()
        {
            var shoppingCards = await _shoppingCardServices.GetShoppingCardsAsync();
            if (shoppingCards == null)
            {
                return NotFound("Alışveriş Sepeti Bulunamadı");
            }
            return Ok(shoppingCards);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShoppingCardById(int id)
        {
            var shoppingCard = await _shoppingCardServices.GetShoppingCardAsync(id);
            if (shoppingCard == null)
            {
                return NotFound("Alışveriş Sepeti Bulunamadı");
            }
            return Ok(shoppingCard);
        }

        [HttpPost]
        public async Task<IActionResult> CreateShoppingCard([FromBody] ShoppingCard shoppingCard)
        {
            if (shoppingCard == null)
            {
                return BadRequest("Geçersiz alışveriş sepeti verisi");
            }
            var createdShoppingCard = await _shoppingCardServices.CreateShoppingCardAsync(shoppingCard);
            return Ok(createdShoppingCard);
        }

        [HttpDelete("{id}")]
        public async Task <IActionResult> DeleteShoppingCard(int id)
        {
            var result = await _shoppingCardServices.DeleteShoppingCardAsync(id);
            if (!result)
            {
                return NotFound("Alışveriş Sepeti Bulunamadı");
            }
            return Ok("Alışveriş Sepeti silindi.");
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] Order order)
         
        {
            if (order == null)
            {
                return BadRequest("Geçersiz alışveriş sepeti verisi");
            }
            var createdOrder = await _shoppingCardServices.AddOrderAsync(order);
            return Ok(createdOrder);
        }
    }

}