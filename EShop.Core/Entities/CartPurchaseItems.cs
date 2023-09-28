namespace EShop.Core.Entities
{
    public class CartPurchaseItems
    {
        public int CustomerId { get; set; }
        public List<CartEntity> Items { get; set; }
    }
}
