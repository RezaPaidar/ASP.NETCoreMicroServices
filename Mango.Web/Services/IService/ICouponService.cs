using Mango.Web.Models;

namespace Mango.Web.Services.IService
{
    public interface ICouponService
    {
        Task<ResponseDTO?> GetCouponAsync(string couponCode);
        Task<ResponseDTO?> GetAllCouponsAsync();
        Task<ResponseDTO?> GetCouponsByIdAsync(int id);
        Task<ResponseDTO?> CreateCouponsAsync(CouponDTO couponDto);
        Task<ResponseDTO?> UpdateCouponsAsync(CouponDTO couponDto);
        Task<ResponseDTO?> DeleteCouponsAsync(int id);

    }
}
