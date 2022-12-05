using CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CouponAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                 new Coupon() 
                 { Id = 1, 
                   Name = "Especial", 
                   Percent = 60, 
                   Active = true 
                 },
                 new Coupon() 
                 { Id = 2, 
                   Name = "Super Especial", 
                   Percent = 90, 
                   Active = false 
                 });
        }
    }
}
