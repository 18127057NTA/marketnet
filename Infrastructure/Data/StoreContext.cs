using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        
        public DbSet<ProductType> ProductTypes { get; set; }
        
        public DbSet<Store> Stores { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}