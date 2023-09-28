using EShop.Application.Dto;
using EShop.Core.Entities;
using MediatR;

namespace EShop.Application.Command
{
    public class GetCartItemQuery : IRequest<List<CartItemEntity>>
    {

        public int CustomerId { get; set; }

    }
}
