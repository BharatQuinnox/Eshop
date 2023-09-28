using AutoMapper;
using EShop.Application.Command;
using EShop.Application.Dto;
using EShop.Application.IServices;
using EShop.Core.Entities;
using MediatR;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EShop.Application.Services
{
    public class ShippingService : IShippingService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public ShippingService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<Dto.Shipping>>> GetShippingData(int CustomerId)
        {
            List<Dto.Shipping> shippinglist = new List<Dto.Shipping>();

            var result = await _mediator.Send(new GetShippingAddressByIdQuery { CustomerId = CustomerId });
            shippinglist = _mapper.Map<List<Dto.Shipping>>(result);
            if (shippinglist!=null && shippinglist.Count > 0)
            {
                return new ApiResponse<List<Dto.Shipping>>()
                {
                    Data = shippinglist,
                    StatusCode = (int)(HttpStatusCode.OK),
                    HasError = false,
                    Error = string.Empty
                };
            }
            else
            {
                return new ApiResponse<List<Dto.Shipping>>()
                {
                    Data = shippinglist,
                    StatusCode = (int)(HttpStatusCode.NoContent),
                    HasError = true,
                    Error = "No data found"
                };
            }
        }

        public async Task<ApiResponse<int>> InsertShippingAddress(Dto.Shipping shipping)
        {
            //convert shipping dto to entity 
            ShippingEntity shippingEntity = new ShippingEntity();
            shippingEntity = _mapper.Map<ShippingEntity>(shipping);
            CreateShippingCommand createShippingCommand = new CreateShippingCommand() { payload = shippingEntity };
            var order = await _mediator.Send(createShippingCommand);
            return new ApiResponse<int>
            {
                Data = order
            };
        }

    }
}
