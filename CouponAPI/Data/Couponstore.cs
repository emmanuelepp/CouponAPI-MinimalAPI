using CouponAPI.Models;

namespace CouponAPI.Data
{
    public static class Couponstore
    {
        public static List<Coupon> CouponsList = new List<Coupon> {
            new Coupon{ Id = 1, Name="Especial", Percent =60, Active = true},
            new Coupon{ Id = 2, Name="Super Especial", Percent =90, Active = false},
            };
    }
}
