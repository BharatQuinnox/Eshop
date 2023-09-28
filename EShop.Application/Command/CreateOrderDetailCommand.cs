using EShop.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Command
{
    public class CreateOrderDetailCommand : IRequest<bool>
    {
        public  List<OrderDetailEntity> payload { get; set; }
    }
}
