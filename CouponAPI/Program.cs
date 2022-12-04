using AutoMapper;
using CouponAPI;
using CouponAPI.Data;
using CouponAPI.Models;
using CouponAPI.Models.DTO;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Endpoints 
app.MapGet("/api/coupon", (ILogger<Program> _logger) =>
{
    _logger.Log(LogLevel.Information, "Get all coupons");
    return Results.Ok(Couponstore.CouponsList);
}).WithName("GetCoupons").Produces<IEnumerable<Coupon>>(200);

app.MapGet("/api/coupon/{id:int}", (int id) =>
{
    return Results.Ok(Couponstore.CouponsList.Where(x => x.Id == id));

}).WithName("GetCoupon").Produces<Coupon>(200);

app.MapPost("/api/coupon", async  (IValidator<CouponCreateDTO> _validation,IMapper _mapper, [FromBody] CouponCreateDTO couponCreateDTO) =>
{
    FluentValidation.Results.ValidationResult validationResult = await _validation.ValidateAsync(couponCreateDTO);

    if (!validationResult.IsValid)
    {
        return Results.BadRequest(validationResult.Errors.FirstOrDefault().ToString());
    }

    if (Couponstore.CouponsList.FirstOrDefault(x => x.Name?.ToLower() == couponCreateDTO.Name?.ToLower()) != null)
    {
        return Results.BadRequest("Coupon Name Already Exists");
    }

    Coupon coupon = _mapper.Map<Coupon>(couponCreateDTO);

    coupon.Id = Couponstore.CouponsList.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;

    Couponstore.CouponsList.Add(coupon);

    CouponDTO couponDTO = _mapper.Map<CouponDTO>(coupon);

    return Results.CreatedAtRoute("GetCoupon", new { id = coupon.Id }, couponDTO);

}).WithName("CreatedCoupon").Accepts<CouponCreateDTO>("application/json").Produces<CouponDTO>(201).Produces(400);

app.MapPut("/api/coupon", () =>
{

});

app.MapDelete("/api/coupon/{id:int}", (int id) =>
{

});

app.Run();