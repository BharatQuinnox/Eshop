using EShop.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Command.Handler
{
    public class CreateOrderDetailCommandHandler : IRequestHandler<CreateOrderDetailCommand, bool>
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        public CreateOrderDetailCommandHandler(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }
        public async Task<bool> Handle(CreateOrderDetailCommand request, CancellationToken cancellationToken)
        {
           return await _orderDetailRepository.AddOrderDetail(request.payload);
        }
    }
}
