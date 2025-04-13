using backend.Context;
using backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace backend.Services
{
    public class ShoppingCardServices{
        private readonly butikContext _butikContext;
        
        public ShoppingCardServices(butikContext butikContext){
            _butikContext = butikContext;
        }

        public async Task<List<ShoppingCardDto>> GetShoppingCardsAsync(){
            var ShoppingCard= await _butikContext.ShoppingCards
            .Include(s => s.User)
            .Include(s => s.Product)
            .Select(s => new ShoppingCardDto
            {
                Id = s.Id,
                Username = s.User.Name,
                ProductName = s.Product.Name,
                Price = s.Product.Price,
                ImageUrl = s.Product.ImageUrl,
                Quantity = s.Quantity,
            }).ToListAsync();

            return ShoppingCard;
           
        }

        public async Task<ShoppingCardDto> GetShoppingCardAsync(int id){
            var shoppingCard = await _butikContext.ShoppingCards
            .Include(s => s.User)
            .Include(s => s.Product)
            .Where(s => s.Id == id)
            .Select(s => new ShoppingCardDto
            {
                Id = s.Id,
                Username = s.User.Name,
                ProductName = s.Product.Name,
                Price = s.Product.Price,
                ImageUrl = s.Product.ImageUrl,
                Quantity = s.Quantity,
            }).FirstOrDefaultAsync();

            return shoppingCard;
           
        }

        public async Task<List<ShoppingCard>> CreateShoppingCardAsync(ShoppingCard shoppingCard){
            if(shoppingCard == null){
                return null;
            }
            
            await _butikContext.ShoppingCards.AddAsync(shoppingCard);
            await _butikContext.SaveChangesAsync();
            return new List<ShoppingCard> { shoppingCard };}

        public async Task<bool> DeleteShoppingCardAsync(int id){
            var shoppingCard = await _butikContext.ShoppingCards.FindAsync(id);
            if(shoppingCard == null){
                return false;
            }
            _butikContext.ShoppingCards.Remove(shoppingCard);
            await _butikContext.SaveChangesAsync();
            return true;
        }

        public async Task<Order> AddOrderAsync(Order order){
             var product = await _butikContext.Products.FirstOrDefaultAsync(p=>p.Id == order.ProductId);
                if(product == null || product.Stock < order.Quantity){
                    return null;
                }

                product.Stock -= order.Quantity;
                _butikContext.Orders.Add(order);
                await _butikContext.SaveChangesAsync();
                return order;

        }

    

    }

   

}