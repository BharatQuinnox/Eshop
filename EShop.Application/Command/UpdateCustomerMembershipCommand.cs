using EShop.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Command
{
    public class UpdateCustomerMembershipCommand : IRequest<bool>
    {
        public CustomerEntity payload { get; set; }
    }
}
