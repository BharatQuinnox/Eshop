using EShop.Application.Command;
using EShop.Application.Dto;
using EShop.Core.Entities;

namespace EShop.Application.IServices
{
    public interface IShippingService
    {
      Task<ApiResponse<int>> InsertShippingAddress(Dto.Shipping shipping);

        Task<ApiResponse<List<Dto.Shipping>>> GetShippingData(int CustomerId);

    }
}
