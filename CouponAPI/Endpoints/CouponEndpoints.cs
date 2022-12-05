using AutoMapper;
using CouponAPI.Models.DTO;
using CouponAPI.Models;
using CouponAPI.Repository;
using FluentValidation;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace CouponAPI.Endpoints
{
    public static class CouponEndpoints
    {
        public static void ConfigureCouponEndpoints(this WebApplication app)
        {
            app.MapGet("/api/coupon", GetAllCoupons).WithName("GetCoupons").Produces<APIResponse>(200);

            app.MapGet("/api/coupon/{id:int}", GetCoupon).WithName("GetCoupon").Produces<APIResponse>(200);

            app.MapPost("/api/coupon", CreateCoupon).WithName("CreatedCoupon").Accepts<CouponCreateDTO>("application/json").Produces<CouponDTO>(201).Produces(400);

            app.MapPut("/api/coupon", UpdateCoupon).WithName("UpdateCoupon").Accepts<CouponUpdateDTO>("application/json").Produces<APIResponse>(200).Produces(400);

            app.MapDelete("/api/coupon/{id:int}", DeleteCoupon);
        }

        private async static Task<IResult> GetCoupon(ICouponRepository couponRepository, int id)
        {
            APIResponse response = new();
            response.Result = await couponRepository.GetAsync(id);
            response.Success = true;
            response.StatusCode = HttpStatusCode.OK;

            return Results.Ok(response);
        }

        private async static Task<IResult> CreateCoupon(ICouponRepository couponRepository, IValidator<CouponCreateDTO> _validation,
            IMapper _mapper, [FromBody] CouponCreateDTO couponCreateDTO)
        {
            APIResponse response = new() { Success = false, StatusCode = HttpStatusCode.BadRequest };
            FluentValidation.Results.ValidationResult validationResult = await _validation.ValidateAsync(couponCreateDTO);

            if (!validationResult.IsValid)
            {
                response.ErrosMessages.Add(validationResult.Errors.FirstOrDefault().ToString());
                return Results.BadRequest(response);
            }

            if (couponRepository.GetAsync(couponCreateDTO.Name).GetAwaiter().GetResult() != null)
            {
                response.ErrosMessages.Add("Coupon Name Already Exists");
                return Results.BadRequest(response);
            }

            Coupon coupon = _mapper.Map<Coupon>(couponCreateDTO);

            await couponRepository.CreateAsync(coupon);
            await couponRepository.SaveAsync();

            CouponDTO couponDTO = _mapper.Map<CouponDTO>(coupon);
            response.Result = couponDTO;
            response.Success = true;
            response.StatusCode = HttpStatusCode.Created;

            return Results.Ok(response);
        }

        private async static Task<IResult> UpdateCoupon(ICouponRepository couponRepository, IValidator<CouponUpdateDTO> _validation,
            IMapper _mapper, [FromBody] CouponUpdateDTO couponUpdateDTO)
        {
            APIResponse response = new();
            FluentValidation.Results.ValidationResult validationResult = await _validation.ValidateAsync(couponUpdateDTO);

            if (!validationResult.IsValid)
            {
                response.ErrosMessages.Add(validationResult.Errors.FirstOrDefault().ToString());
                return Results.BadRequest(response);
            }

            await couponRepository.UpdateAsync(_mapper.Map<Coupon>(couponUpdateDTO));
            await couponRepository.SaveAsync();

            response.Result = _mapper.Map<CouponDTO>(await couponRepository.GetAsync(couponUpdateDTO.Id));
            response.Success = true;
            response.StatusCode = HttpStatusCode.OK;

            return Results.Ok(response);
        }

        private async static Task<IResult> GetAllCoupons(ICouponRepository couponRepository, ILogger<Program> _logger)
        {
            APIResponse response = new();
            _logger.Log(LogLevel.Information, "Get all coupons");
            response.Result = await couponRepository.GetAllAsync();
            response.Success = true;
            response.StatusCode = HttpStatusCode.OK;

            return Results.Ok(response);
        }

        private async static Task<IResult> DeleteCoupon(ICouponRepository couponRepository, int id)
        {
            APIResponse response = new();
            Coupon couponFromStore = await couponRepository.GetAsync(id);

            if (couponFromStore != null)
            {
                await couponRepository.RemoveAsync(couponFromStore);
                await couponRepository.SaveAsync();
                response.Success = true;
                response.StatusCode = HttpStatusCode.NoContent;
                return Results.Ok(response);
            }
            else
            {
                response.ErrosMessages.Add("Invalid id");
                return Results.BadRequest(response);
            }
        }
    }
}
