using ECommerceApp.Domain.ValueObjects;

namespace ECommerceApp.Domain.Entities
{
    public class CartItemEntity
    {
        public CartItemEntity(int productId, int cartId, Quantity quantity, ProductEntity product) {
            ProductId = productId;
            CartId = cartId;
            Quantity = quantity;
            Product = product;
        }
        public int Id { get; set; }
        public int ProductId { get; }
        public int CartId { get; }
        public CartEntity CartEntity { get; }
        public Quantity Quantity { get; }
        public ProductEntity? Product { get; }
    }
}
