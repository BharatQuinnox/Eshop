using EShop.Core.Entities;

namespace EShop.Core.Interfaces
{
    public interface IProductRepository
    {
       Task<List<ProductEntity>> GetProductList();
    }
}
