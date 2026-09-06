namespace ECommerceApp.Infrastructure.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; } = null!;

        public string ShippingCity { get; set; } = string.Empty;

        public string ShippingStreet { get; set; } = string.Empty;

        public string ShippingHouseNumber { get; set; } = string.Empty;

        public string ShippingZipCode { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; }

        public decimal DiscountAmount { get; set; }

        public DateTime OrderDate { get; set; }

        public List<OrderItem> Items { get; set; } = new();
    }
}
