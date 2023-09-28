using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Infrastructure.DataModel
{
    public class ProductDataModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public bool IsActive { get; set; }


        public const string SelectQuery = "SELECT ProductId, Name, Price, IsActive FROM Product";

    }
}
