namespace EShop.Core.Entities
{
    public class ShippingSlipEntity
    {
        public List<OrderEntity> orderDetails { get; set; }
        public List<ShippingEntity> shipping { get; set; }
    }
}
