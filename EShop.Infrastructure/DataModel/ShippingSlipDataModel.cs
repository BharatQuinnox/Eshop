using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Infrastructure.DataModel
{
    public class ShippingSlipDataModel
    {

        public string ProductName { get; set; }
        public string CustomerName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime TransactionDate { get; set; }


        public const string Insert = "INSERT INTO [Order](OrderNumber, CustomerId, TotalAmount, TransactionDate, IsActive, ShipId) VALUES(@OrderNumber, @CustomerId, @TotalAmount, @TransactionDate, @IsActive, @ShipId)" +
                              "SELECT CAST(SCOPE_IDENTITY() AS INT)";

        public const string SelectQuery = " SELECT p.Name as ProductName, c.Name as CustomerName, od.Quantity," +
                                          " o.TotalAmount, o.TransactionDate FROM [dbo].[Order] o " +
                                          " LEFT JOIN OrderDetail od ON o.OrderId = od.OrderId " +
                                          " LEFT JOIN Customer c ON c.CustomerId = o.CustomerId " +
                                          " LEFT JOIN dbo.ShippingDetail sd ON o.ShipId = sd.ShippingId " +
                                          " LEFT JOIN Product p ON p.ProductId = od.ProductId " +
                                          " WHERE o.OrderId = @Id AND od.IsMembership = 0;";
    }
}
