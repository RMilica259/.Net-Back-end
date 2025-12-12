using Microsoft.EntityFrameworkCore;
using ECommerceApp.Infrastructure.Models;

namespace ECommerceApp.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<CartItem> CartItems { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Cart> Carts { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .OwnsOne(o => o.Address, a =>
                {
                    a.Property(x => x.City).HasColumnName("City");
                    a.Property(x => x.Street).HasColumnName("Street");
                    a.Property(x => x.HouseNumber).HasColumnName("HouseNumber");
                    a.Property(x => x.ZipCode).HasColumnName("ZipCode");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
