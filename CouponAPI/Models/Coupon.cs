﻿namespace CouponAPI.Models
{
    public class Coupon
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Percent { get; set; }
        public bool Active { get; set; } 
        public DateTime? Created { get; set; }
        public DateTime? LastUpdate { get; set; } 

    }
}
