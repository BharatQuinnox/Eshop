using EShop.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Command
{
    public class DeleteCartCommand : IRequest<bool>
    {
        public CartItemEntity payload { get; set; }

    }
}
