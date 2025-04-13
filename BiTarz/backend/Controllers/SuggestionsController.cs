using backend.Services;
using Microsoft.AspNetCore.Mvc;
using backend.Entities;

namespace backend.Controllers
{
    public class SuggestionsController : ControllerBase
    {
        private readonly SuggestionsServices _suggestionsServices;
        public SuggestionsController(SuggestionsServices suggestionsServices)
        {
            _suggestionsServices = suggestionsServices;
        }
     
        [HttpGet]
        public async Task <IActionResult> GetActionResultAsync(){
            var suggestions = await _suggestionsServices.GetSuggestionsAsync();
            if (suggestions == null)
            {
                return NotFound();
            }
            return Ok(suggestions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSuggestionsById(int id)
        {
            var suggestions = await _suggestionsServices.GetSuggestionsAsync(id);
            if (suggestions == null)
            {
                return NotFound("Öneri Bulunamadı");
            }
            return Ok(suggestions);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSuggestions([FromBody] Suggestions suggestions)
        {
            if (suggestions == null)
            {
                return BadRequest("Geçersiz öneri verisi");
            }
            var createdSuggestions = await _suggestionsServices.CreateSuggestionsAsync(suggestions);
            return Ok(createdSuggestions);
        }





    }

}