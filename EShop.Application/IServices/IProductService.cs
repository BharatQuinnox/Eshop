using EShop.Application.Command;
using EShop.Application.Dto;
using EShop.Core.Entities;

namespace EShop.Application.IServices
{
    public interface IProductService
    {
        Task<ApiResponse<List<Product>>> GetProductList();
    }
}
