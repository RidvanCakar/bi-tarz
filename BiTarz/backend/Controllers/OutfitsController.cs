using backend.Services;
using Microsoft.AspNetCore.Mvc;
using backend.Entities;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OutfisController : ControllerBase
    {
        private readonly OutfitsServices _outfitsServices;
        public OutfisController(OutfitsServices outfitsServices)
        {
            _outfitsServices = outfitsServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOutfits()
        {
            var outfis = await _outfitsServices.GetOutfitsAsync();
            if (outfis == null)
            {
                return NotFound("Outfit Bulunamadı");
            }
            return Ok(outfis);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOutfitById(int id)
        {
            var outfit = await _outfitsServices.GetOutfitAsync(id);
            if (outfit == null)
            {
                return NotFound("Outfit Bulunamadı");
            }
            return Ok(outfit);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOutfit([FromBody] Outfit outfit)
        {
            if (outfit == null)
            {
                return BadRequest("Geçersiz outfit verisi");
            }
            var createdOutfit = await _outfitsServices.CreateOutfitAsync(outfit);
            return Ok(createdOutfit);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOutfit(int id)
        {
            var result = await _outfitsServices.DeleteOutfitAsync(id);
            if (!result)
            {
                return NotFound("Outfit Bulunamadı");
            }
            return Ok("Outfit silindi.");

        }

    }
}