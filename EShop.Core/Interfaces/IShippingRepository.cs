using EShop.Core.Entities;

namespace EShop.Core.Interfaces
{
    public interface IShippingRepository
    {
       Task<int> InsertShippingData(ShippingEntity shipping);
        Task<List<ShippingEntity>> GetShippingData(int CustomerId);

    }
}
