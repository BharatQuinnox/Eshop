using EShop.Application.Dto;
using EShop.Core.Entities;
using MediatR;

namespace EShop.Application.Order
{
    public class CreateOrderCommand : IRequest<int>
    {
        public OrderEntity payload { get; set; }
    }
}
