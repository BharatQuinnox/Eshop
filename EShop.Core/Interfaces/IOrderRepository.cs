using EShop.Core.Entities;

namespace OnlineLibraryShop.Core.Interfaces
{
    public interface IOrderRepository
    {
        Task<int> CreateOrder(OrderEntity purchaseRequest);

        Task<List<OrderEntity>> GetOrder(int PurchaseOrderId);

    }
}
