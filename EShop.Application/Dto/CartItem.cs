using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Dto
{
    public class CartItem 
    {
        public int Type { get; set; }
        public int Quantity { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
