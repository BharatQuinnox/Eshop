
using EShop.Core;
using EShop.Core.Entities;
using MediatR;

namespace EShop.Application.Command
{
    public class GetShippingAddressByIdQuery : IRequest<List<ShippingEntity>>
    {
        public int CustomerId { get; set; }

    }
}
