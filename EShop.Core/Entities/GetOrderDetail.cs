namespace EShop.Core.Entities
{
    public class GetOrderDetail
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string CustomerName { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
