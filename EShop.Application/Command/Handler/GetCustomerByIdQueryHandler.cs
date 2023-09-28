using EShop.Application.Dto;
using EShop.Core.Entities;
using EShop.Core.Interfaces;
using MediatR;
using System.Net;

namespace EShop.Application.Command.Handler
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerEntity>
    {
        private readonly ICustomerRepository _customerRepository;
        public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<CustomerEntity> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {

            var customer = await _customerRepository.GetCustomerById(request.CustomerId);
            return customer;
             
        }
    }
}
