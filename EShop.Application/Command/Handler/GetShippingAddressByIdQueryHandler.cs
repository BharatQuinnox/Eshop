using EShop.Application.Dto;
using EShop.Core.Entities;
using EShop.Core.Interfaces;
using MediatR;
using System.Net;

namespace EShop.Application.Command.Handler
{
    public class GetShippingAddressByIdQueryHandler : IRequestHandler<GetShippingAddressByIdQuery, List<ShippingEntity>>
    {
        private readonly IShippingRepository _shippingRepository;
        public GetShippingAddressByIdQueryHandler(IShippingRepository shippingRepository)
        {
            _shippingRepository = shippingRepository;
        }

        public async Task<List<ShippingEntity>> Handle(GetShippingAddressByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _shippingRepository.GetShippingData(request.CustomerId);
            return result;
        }
    }
}
