using backend.Context;
using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class SuggestionsServices
    {
        private readonly butikContext _butikContext;
        public SuggestionsServices(butikContext butikContext)
        {
            _butikContext = butikContext;
        }


        public async Task<List<Suggestions>> GetSuggestionsAsync()
        {
            var suggestions = await _butikContext.Suggestions.ToListAsync();
            if (suggestions == null)
            {
                return null;
            }
            return suggestions;
        }

        public async Task<Suggestions> GetSuggestionsAsync(int id){
             var suggestions = await _butikContext.Suggestions.FindAsync(id);
             if(suggestions == null){
                 return null;
             }
             return suggestions;
        }

        public async Task<Suggestions> CreateSuggestionsAsync(Suggestions suggestions){
            if(suggestions ==null){
                return null;
            }

            await _butikContext.Suggestions.AddAsync(suggestions);
            await _butikContext.SaveChangesAsync();
            return suggestions;
            
        }
    }

}