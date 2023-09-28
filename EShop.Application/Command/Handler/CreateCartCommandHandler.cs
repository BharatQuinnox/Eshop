using AutoMapper;
using EShop.Application.Dto;
using EShop.Core.Entities;
using EShop.Core.Interfaces;
using MediatR;
using System.Net;

namespace EShop.Application.Command.Handler
{
    public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, bool>
    {
        private readonly ICartRepository _cartRepository;
         private readonly IMapper _mapper;
        public CreateCartCommandHandler(ICartRepository cartRepository, IMapper mapper)
        {
            _mapper = mapper;
            _cartRepository = cartRepository;
        }
        public async Task<bool> Handle(CreateCartCommand command, CancellationToken cancellationToken)
        {
           
            var CartId = await _cartRepository.AddCartItem(command.payload);
            return CartId;

        }
    }
}
