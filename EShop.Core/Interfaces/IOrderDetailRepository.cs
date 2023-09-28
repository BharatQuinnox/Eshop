using EShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.Interfaces
{
    public interface IOrderDetailRepository
    {
        Task<bool> AddOrderDetail(List<OrderDetailEntity> orderDetailEntity);

        
    }
}
