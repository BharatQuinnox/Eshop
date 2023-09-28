using AutoMapper;
using EShop.Application.Command;
using EShop.Application.Dto;
using EShop.Application.IServices;
using EShop.Core.Entities;
using MediatR;
using System.Net;

namespace EShop.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;


        public CustomerService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<ApiResponse<Customer>> GetCustomerById(Customer customer)
        {

            var result = await _mediator.Send(new GetCustomerByIdQuery { CustomerId = customer.CustomerId });
            var customerDetails = _mapper.Map<Customer>(result);

            if (customerDetails != null)
            {
                return new ApiResponse<Customer>()
                {
                    Data = customerDetails,
                    Error = "",
                    HasError = false,
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            else
            {
                return new ApiResponse<Customer>()
                {
                    Data = customerDetails,
                    Error = "",
                    HasError = false,
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }

        }


    }
}
