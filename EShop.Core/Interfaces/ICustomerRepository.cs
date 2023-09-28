using EShop.Core.Entities;

namespace EShop.Core.Interfaces
{
    public interface ICustomerRepository
    {
         Task<CustomerEntity> GetCustomerById(int CustomerId);
        Task<bool> ActiveCustomerMemberShip(int CustomerId);


    }
}
