using AutoMapper;
using EShop.Application.Dto;
using EShop.Core.Entities;
using EShop.Core.Interfaces;
using MediatR;
using System.Net;

namespace EShop.Application.Command.Handler
{
    public class CreateShippingCommandHandler : IRequestHandler<CreateShippingCommand, int>
    {
        private readonly IShippingRepository _shippingRepository;
        private readonly IMapper _mapper;

        public CreateShippingCommandHandler(IShippingRepository shippingRepository, IMapper mapper)
        {
            _shippingRepository = shippingRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateShippingCommand request, CancellationToken cancellationToken)
        {
             
            var result = await _shippingRepository.InsertShippingData(request.payload);
            return result;
            
        }
    }
}
