﻿namespace CouponAPI.Models.DTO
{
    public class CouponDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Percent { get; set; }
        public bool Active { get; set; }
        public DateTime? Created { get; set; }
    }
}
