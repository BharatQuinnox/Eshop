using EShop.Core.Interfaces;
using MediatR;

namespace EShop.Application.Command.Handler
{
    public class DeleteCartCommandHandler : IRequestHandler<DeleteCartCommand, bool>
    {
        public readonly ICartRepository _cartRepository;
        public DeleteCartCommandHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<bool> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            return await _cartRepository.Delete(request.payload.CustomerId.Value);
        }
    }
}
