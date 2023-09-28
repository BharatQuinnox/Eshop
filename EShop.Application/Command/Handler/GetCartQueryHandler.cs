using EShop.Application.Dto;
using EShop.Core.Entities;
using EShop.Core.Interfaces;
using MediatR;
using System.Net;

namespace EShop.Application.Command.Handler
{
    public class GetCartQueryHandler : IRequestHandler<GetCartItemQuery, List<CartItemEntity>>
    {
        private readonly ICartRepository _cartRepository;
        public GetCartQueryHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<List<CartItemEntity>> Handle(GetCartItemQuery request, CancellationToken cancellationToken)
        {
            var result = await _cartRepository.getCartItem(request.CustomerId);
            return result;

        }
    }
}
