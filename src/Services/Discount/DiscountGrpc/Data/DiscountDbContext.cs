using DiscountGrpc.Models;
using Microsoft.EntityFrameworkCore;

namespace DiscountGrpc.Data
{
    public class DiscountDbContext : DbContext
    {
        public DiscountDbContext(DbContextOptions<DiscountDbContext> options) : base(options)
        {

        }

        public DbSet<Coupon> Coupons { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Coupon>()
                .HasData(
                    new List<Coupon>
                    {
                        new Coupon { Id = 1, ProductName = "Product 1", Description = "this product 4 test", Amount = 1 },
                        new Coupon { Id = 2, ProductName = "Product 2", Description = "this product 4 test", Amount = 3 },
                        new Coupon { Id = 3, ProductName = "Product 3", Description = "this product 4 test", Amount = 2 }

                    });
            base.OnModelCreating(modelBuilder);
        }
    }
}
