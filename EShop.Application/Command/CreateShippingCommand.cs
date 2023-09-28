using EShop.Application.Dto;
using EShop.Core.Entities;
using MediatR;

namespace EShop.Application.Command
{
    public class CreateShippingCommand : IRequest<int>
    {
      public  ShippingEntity payload { get; set; }
    }
}
