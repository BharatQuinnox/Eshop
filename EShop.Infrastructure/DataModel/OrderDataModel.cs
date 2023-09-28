using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Infrastructure.DataModel
{
    public class OrderDataModel
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }
        public int ShipId { get; set; }

        public const string Insert = "INSERT INTO [Order](OrderNumber, CustomerId, TotalAmount, TransactionDate, IsActive, ShipId) VALUES(@OrderNumber, @CustomerId, @TotalAmount, @TransactionDate, @IsActive, @ShipId)" +
                              "SELECT CAST(SCOPE_IDENTITY() AS INT)";

    }
}
