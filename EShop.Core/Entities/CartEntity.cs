using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.Entities
{
    public class CartEntity
    {
        public List<CartItemEntity> Items { get; set; }
    }
}
