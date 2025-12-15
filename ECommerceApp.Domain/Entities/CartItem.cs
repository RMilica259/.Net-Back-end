using ECommerceApp.Domain.ValueObjects;

namespace ECommerceApp.Domain.Entities
{
    public class CartItemEntity
    {
        public CartItemEntity(int customerId, int productId, Quantity quantity, ProductEntity product) {
            CustomerId = customerId;
            ProductId = productId;
            Quantity = quantity;
            Product = product;
        }
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; }
        public int CartId { get; }
        public CartEntity CartEntity { get; }
        public Quantity Quantity { get; }
        public ProductEntity? Product { get; }
    }
}
