using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Infrastructure.DataModel
{
    public class OrderDetailDataModel
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal NetAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool IsMembership { get; set; }
        public bool IsActive { get; set; }


        public const string InsertQuery = "INSERT INTO OrderDetail (OrderId, ProductId, Quantity, DiscountAmount, NetAmount, TransactionDate, IsMembership, IsActive) " +
                             "VALUES (@OrderId, @ProductId, @Quantity, @DiscountAmount, @NetAmount, @TransactionDate, @IsMembership, @IsActive)";
    }
}
