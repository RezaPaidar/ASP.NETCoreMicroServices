using Mango.Web.Models;
using Mango.Web.Models.Dto;

namespace Mango.Web.Services.IService
{
    public interface IProductService
    {
        Task<ResponseDTO?> GetProductAsync(string productCode);
        Task<ResponseDTO?> GetAllProductsAsync();
        Task<ResponseDTO?> GetProductByIdAsync(int id);
        Task<ResponseDTO?> CreateProductsAsync(ProductDto productDto);
        Task<ResponseDTO?> UpdateProductsAsync(ProductDto productDto);
        Task<ResponseDTO?> DeleteProductsAsync(int id);

    }
}
