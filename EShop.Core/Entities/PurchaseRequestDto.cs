namespace EShop.Core.Entities
{
    public class PurchaseRequestDto
    {
        public int CustomerId { get; set; }
        public int ShipId { get; set; }
        public bool IncludeLoyaltyMembership { get; set; }


    }

    

}