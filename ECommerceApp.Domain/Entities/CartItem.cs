namespace ECommerceApp.Domain.Entities
{
    public class CartItemEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public ProductEntity? Product { get; set; }
    }
}
