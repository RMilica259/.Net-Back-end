namespace ECommerceApp.Domain.Entities
{
    public class OrderEntity
    {
            public OrderEntity(int customerId, AddressEntity shippingAddress, string phoneNumber, decimal totalAmount, decimal discountAmount, DateTime orderDate)
            {
                CustomerId = customerId;
                ShippingAddress = shippingAddress;
                PhoneNumber = phoneNumber;
                TotalAmount = totalAmount;
                DiscountAmount = discountAmount;
                OrderDate = orderDate;
        }

        public int CustomerId { get; }
        public AddressEntity ShippingAddress { get; } 
        public string PhoneNumber { get; } 
        public decimal TotalAmount { get; }
        public decimal DiscountAmount { get; }
        public DateTime OrderDate { get; } 

    }
}
