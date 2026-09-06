using ECommerceApp.Domain.ValueObjects;

namespace ECommerceApp.Domain.Entities
{
    public class OrderItemEntity
    {
        public OrderItemEntity(
            int productId,
            decimal unitPrice,
            Quantity quantity)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        public int Id { get; set; }

        public int ProductId { get; }

        public decimal UnitPrice { get; }

        public Quantity Quantity { get; }
    }
}
