using AutoMapper;
using EShop.Application.Dto;
using EShop.Application.Order;
using EShop.Core.Entities;
using MediatR;
using OnlineLibraryShop.Core.Interfaces;
using System.Net;

namespace OnlineLibraryShop.Application.Command.Handler
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateOrderCommand command, CancellationToken cancellationtoken)
        {
           // var purchaseRequestDto = _mapper.Map<PurchaseRequestDto>(command);
            int Orderid = await _orderRepository.CreateOrder(command.payload);
            return Orderid;
             
        }

    }
}
