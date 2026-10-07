using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService(ApplicationDbContext db, ILogger<DiscountService> logger) 
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> Get(GetRequest request, ServerCallContext context)
    {
        var model = await db
            .Coupons
            .FirstOrDefaultAsync(c => c.ProductName == request.ProductName);

        if (model is null)
            model = new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };

        logger.LogInformation("Discount is retrieved for ProductName : {productName}, Amount : {amount}", 
            model.ProductName, model.Amount);

        var couponModel = model.Adapt<CouponModel>();
        return couponModel;
    }
    public override async Task<CouponModel> Create(CreateRequest request, ServerCallContext context)
    {
        var model = request.Coupon.Adapt<Coupon>();
        if (model is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

        db.Coupons.Add(model);
        await db.SaveChangesAsync();

        logger.LogInformation("Discount is successfully created. ProductName : {ProductName}", model.ProductName);

        var couponModel = model.Adapt<CouponModel>();
        return couponModel;
    }
    public override async Task<CouponModel> Update(UpdateRequest request, ServerCallContext context)
    {
        var model = request.Coupon.Adapt<Coupon>();
        if (model is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

        db.Coupons.Update(model);
        await db.SaveChangesAsync();

        logger.LogInformation("Discount is successfully updated. ProductName : {ProductName}", model.ProductName);

        var couponModel = model.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<DeleteResponse> Delete(DeleteRequest request, ServerCallContext context)
    {
        var model = await db
            .Coupons
            .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

        if (model is null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));

        db.Coupons.Remove(model);
        await db.SaveChangesAsync();

        logger.LogInformation("Discount is successfully deleted. ProductName : {ProductName}", request.ProductName);

        return new DeleteResponse { Success = true };
    }
}