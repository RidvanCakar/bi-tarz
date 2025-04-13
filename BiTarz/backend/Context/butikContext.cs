using System.Data.Common;
using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Context{
    public class butikContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Outfit> Outfits { get; set; }
        public DbSet<Suggestions> Suggestions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ShoppingCard> ShoppingCards { get; set; }
        
        public butikContext(DbContextOptions<butikContext> options) : base(options) { }
    
    }
}