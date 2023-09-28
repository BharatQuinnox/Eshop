namespace EShop.Core.Entities
{
    public class ProductEntity
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        
        public bool IsActive { get; set; }
    }
}
