using CouponAPI.Models.DTO;
using FluentValidation;

namespace CouponAPI.Validations
{
    public class CouponUpdateValidation : AbstractValidator<CouponUpdateDTO>
    {
        public CouponUpdateValidation() 
        {
            RuleFor(m => m.Id).NotEmpty().GreaterThan(0);
            RuleFor(m => m.Name).NotEmpty();
            RuleFor(m => m.Percent).InclusiveBetween(1, 100);
        }
    }
}
