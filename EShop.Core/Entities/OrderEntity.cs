namespace EShop.Core.Entities
{
    public class OrderEntity
    {
        public int? CustomerId { get; set; }
        public int ShipId { get; set; }
        public bool IncludeLoyaltyMembership { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal TotalAmount { get; set; }

        public string CustomerName { get; set; }

        public DateTime? TransactionDate { get; set; }

        public string OrderNumber { get; set; }
        public bool IsActive { get; set; }

        public decimal Price { get; set; }


    }
}
