using EShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Infrastructure.DataModel
{
    public class CartDataModel
    {
        public int CustomerId { get; set; }
        public List<CartItemEntity> Items { get; set; }
    }
}
