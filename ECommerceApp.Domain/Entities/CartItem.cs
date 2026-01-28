using ECommerceApp.Domain.ValueObjects;

namespace ECommerceApp.Domain.Entities
{
    public class CartItemEntity
    {
        public CartItemEntity(int productId, decimal price, Quantity quantity) {
            ProductId = productId;
            Price = price;
            Quantity = quantity;
        }

        public int ProductId { get; }
        public decimal Price { get; }
        public Quantity Quantity { get; private set; }

        public decimal TotalPrice() => Price * Quantity.Value;

        public void IncreaseQuantity(Quantity quantity)
        {
            Quantity = Quantity.Add(quantity);
        }
    }
}
