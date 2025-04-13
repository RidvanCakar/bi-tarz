using backend.Context;
using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class OutfitsServices{
        private readonly butikContext _butikContext;

        public OutfitsServices(butikContext butikContext)
        {
            _butikContext = butikContext;
        }

        public async Task<List<Outfit>> GetOutfitsAsync(){
            var outfis=await _butikContext.Outfits.ToListAsync();
            if(outfis == null){
                return null;
            }
            return outfis;
        }

        public async Task<Outfit> GetOutfitAsync(int id){
            var outfit = await _butikContext.Outfits.FindAsync(id);
            if(outfit == null){
                return null;
            }
            return outfit;
        }

        public async Task<Outfit> CreateOutfitAsync(Outfit outfit){
            if(outfit == null){
                return null;
            }
            
            await _butikContext.Outfits.AddAsync(outfit);
            await _butikContext.SaveChangesAsync();
            return outfit;
        }

        public async Task<bool> DeleteOutfitAsync(int id){
            var outfit = await _butikContext.Outfits.FindAsync(id);
            if(outfit == null){
                return false;
            }
            _butikContext.Outfits.Remove(outfit);
            await _butikContext.SaveChangesAsync();
            return true;
        }



    }
}