using EShop.Application.Command;
using EShop.Application.Dto;
using EShop.Application.Order;
using EShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.IServices
{
    public interface ICustomerService
    {
        Task<ApiResponse<Customer>> GetCustomerById(Customer getOrderByIdQuery);
    }
}
