using CouponAPI.Models.DTO;
using FluentValidation;

namespace CouponAPI.Validations
{
    public class CouponCreateValidation : AbstractValidator<CouponCreateDTO>
    {
        public CouponCreateValidation()
        {
            RuleFor(m => m.Name).NotEmpty();
            RuleFor(m => m.Percent).InclusiveBetween(1, 100);
        }
    }
}
