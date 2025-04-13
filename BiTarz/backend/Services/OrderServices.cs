using System.ComponentModel;
using backend.Context;
using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class OrderServices
    {
        private readonly butikContext _butikContext;

        public OrderServices(butikContext butikContext)
        {
            _butikContext = butikContext;
        }

        public async Task<List<OrderDto>> GetOrdersAsync()
        {

            var orders = await _butikContext.Orders
            .Include(o => o.User)
            .Include(o => o.Product)
            .Select(o => new OrderDto
            {
                Id = o.Id,
                UserName = o.User.Name,
                ProductName = o.Product.Name,
                Price = o.Product.Price,
                ImageUrl = o.Product.ImageUrl,
                Quantity = o.Quantity,
                OrderNumber = o.OrderNumber,

            }).ToListAsync();
            return orders;
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var order = await _butikContext.Orders
            .Include(o => o.User)
            .Include(o => o.Product)
            .Where(o => o.Id == id)
            .Select(o => new OrderDto
            {
                Id = o.Id,
                UserName = o.User.Name,
                ProductName = o.Product.Name,
                Price = o.Product.Price,
                ImageUrl = o.Product.ImageUrl,
                Quantity = o.Quantity,
                OrderNumber = o.OrderNumber,

            }).FirstOrDefaultAsync();
            return order;
        }

        public async Task<List<Order>> CreateOrderAsync(List<Order> order)
        {
            using (var transaction = await _butikContext.Database.BeginTransactionAsync())
            {
                try
                {
                    foreach (var cartItem in order)
                    {
                        var product = await _butikContext.Products
                        .FirstOrDefaultAsync(p => p.Id == cartItem.ProductId);
                        if (product == null || product.Stock < cartItem.Quantity)
                        {
                            await transaction.RollbackAsync();
                            return null;
                        }

                         product.Stock -= cartItem.Quantity;
                        _butikContext.Products.Update(product);
                        _butikContext.Orders.Add(cartItem);

                    }

                    await _butikContext.SaveChangesAsync();
                    await transaction.CommitAsync();

                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }

                return order;
            }

        }



    }
}