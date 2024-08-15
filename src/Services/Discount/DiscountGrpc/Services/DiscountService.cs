using DiscountGrpc.Data;
using DiscountGrpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace DiscountGrpc.Services
{
    public class DiscountService(DiscountDbContext dbContext, ILogger<DiscountService> logger) : DiscountGrpc.DiscountService.DiscountServiceBase
    {
        public override async Task<CouponDto> GetCoupon(GetCouponRequest request, ServerCallContext context)
        {
            var coupon =
                await dbContext.Coupons.FirstOrDefaultAsync(x =>
                    x.ProductName.ToLower() == request.ProductName.ToLower());
            if (coupon == null)
                return new CouponDto { ProductName = "No Discount", Amount = 0, Description = "No Discount" };
            return coupon.Adapt<CouponDto>();
        }

        public override async Task<CouponDto> CreateCoupon(CreateCouponRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon == null)
                throw new RpcException(status: new Status(StatusCode.InvalidArgument, "Invalid coupon object.!"));
            dbContext.Coupons.Add(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation($"coupon created successfully. product: {request.Coupon.ProductName}.!");
            return coupon.Adapt<CouponDto>();
        }

        public override async Task<CouponDto> UpdateCoupon(UpdateCouponRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            dbContext.Coupons.Update(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation($"coupon updated successfully. product: {request.Coupon.ProductName}.!");
            return coupon.Adapt<CouponDto>();
        }

        public override async Task<DeleteCouponResponse> DeleteCoupon(DeleteCouponRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FindAsync(request.Id);
            if (coupon == null)
                throw new RpcException(status: new Status(StatusCode.NotFound, "coupon not found.!"));
            dbContext.Coupons.Remove(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation($"coupon deleted successfully. product: {coupon.ProductName}.!");
            return new DeleteCouponResponse { Success = true };
        }
    }
}
