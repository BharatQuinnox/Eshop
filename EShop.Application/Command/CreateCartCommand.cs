using EShop.Application.Dto;
using EShop.Core.Entities;
using MediatR;

namespace EShop.Application.Command
{
    public class CreateCartCommand : IRequest<bool>
    {
       public  CartEntity payload { get; set; }
    }
}
