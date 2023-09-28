using EShop.Application.Dto;
using EShop.Application.Order;
using EShop.Core.Entities;
using MediatR;
using OnlineLibraryShop.Core.Interfaces;
using System.Net;

namespace OnlineLibraryShop.Application.Command.Handler
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, List<OrderEntity>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<List<OrderEntity>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _orderRepository.GetOrder(request.OrderId);
            return result;
             
        }
    }
}
