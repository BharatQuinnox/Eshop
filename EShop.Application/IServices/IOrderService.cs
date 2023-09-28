using EShop.Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Application.IServices
{
    public interface IOrderService
    {
        Task<ApiResponse<int>> CreatePurchaseOrder(Dto.Order order);

        Task<bool> GenerateSlipIfRequired(Dto.Order order);

        Task<ApiResponse<List<string>>> ValidateOrderData(Dto.Order order);



    }
}
