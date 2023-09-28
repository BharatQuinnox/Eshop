using EShop.Application.Dto;
using EShop.Core.Entities;
using MediatR;

namespace EShop.Application.Command
{
    public class GetProductByQuery : IRequest<List<ProductEntity>>
    {
    }
}
