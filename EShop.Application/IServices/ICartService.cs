using EShop.Application.Command;
using EShop.Application.Dto;
using EShop.Core.Entities;

namespace EShop.Application.IServices
{
    public interface ICartService
    {
        Task<ApiResponse<bool>> AddCartItems(Cart cart);

        Task<ApiResponse<List<Dto.CartItem>>> GetCartItem(int CustomerId);
        Task<ApiResponse<List<string>>> ValidatCartInvalidData(Cart cart);
    }
}
