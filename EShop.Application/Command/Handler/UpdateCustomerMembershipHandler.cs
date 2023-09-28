using EShop.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Command.Handler
{
    public class UpdateCustomerMembershipHandler : IRequestHandler<UpdateCustomerMembershipCommand, bool>
    {
        private readonly ICustomerRepository _customerRepository;
        public UpdateCustomerMembershipHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<bool> Handle(UpdateCustomerMembershipCommand request, CancellationToken cancellationToken)
        {
            return await _customerRepository.ActiveCustomerMemberShip(request.payload.CustomerId);
        }
    }
}
