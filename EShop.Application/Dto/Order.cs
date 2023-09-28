namespace EShop.Application.Dto
{
    public class Order 
    {
        public int CustomerId { get; set; }
        public int ShipId { get; set; }
        public bool IncludeLoyaltyMemberShip { get; set; }
        public int OrderId { get; set; }
    }
}
