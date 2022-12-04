using System.ComponentModel.DataAnnotations;

namespace CouponAPI.Models.DTO
{
    public class CouponCreateDTO
    {
        public string? Name { get; set; }
        public int Percent { get; set; }
        public bool Active { get; set; }
    }
}
