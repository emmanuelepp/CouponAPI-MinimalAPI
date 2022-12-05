using CouponAPI.Data;
using CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CouponAPI.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly ApplicationDbContext   _context; 

        public CouponRepository( ApplicationDbContext context)
        {
            _context= context;
        }

        public async Task CreateAsync(Coupon coupon)
        {
            _context.Add(coupon);
        }

        public async Task<ICollection<Coupon>> GetAllAsync()
        {
            return await _context.Coupons.ToListAsync();
        }

        public async Task<Coupon> GetAsync(int id)
        {
            return await _context.Coupons.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Coupon> GetAsync(string couponName)
        {
            return await _context.Coupons.FirstOrDefaultAsync(x => x.Name.ToLower() == couponName.ToLower());
        }

        public async Task RemoveAsync(Coupon coupon)
        {
              _context.Remove(coupon);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Coupon coupon)
        {
            _context.Coupons.Update(coupon);
        }
    }
}
