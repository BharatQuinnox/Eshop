using EShop.Application.Dto;
using EShop.Core.Entities;
using MediatR;

namespace EShop.Application.Order
{
    public class GetOrderByIdQuery : IRequest<List<OrderEntity>>
    {
        public int OrderId { get; set; }
    }
}
