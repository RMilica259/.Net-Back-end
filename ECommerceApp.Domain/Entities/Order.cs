namespace ECommerceApp.Domain.Entities
{
    public class OrderEntity
    {
        private readonly HashSet<OrderItemEntity> items = new();

        public OrderEntity(
            int customerId,
            string shippingCity,
            string shippingStreet,
            string shippingHouseNumber,
            string shippingZipCode,
            string phoneNumber,
            decimal totalAmount,
            decimal discountAmount,
            DateTime orderDate,
            IEnumerable<OrderItemEntity> items)
        {
            ArgumentNullException.ThrowIfNull(items);

            var orderItems = items.ToList();

            if (orderItems.Count == 0)
            {
                throw new ArgumentException(
                    "An order must contain at least one item.",
                    nameof(items));
            }

            CustomerId = customerId;
            ShippingCity = shippingCity;
            ShippingStreet = shippingStreet;
            ShippingHouseNumber = shippingHouseNumber;
            ShippingZipCode = shippingZipCode;
            PhoneNumber = phoneNumber;
            TotalAmount = totalAmount;
            DiscountAmount = discountAmount;
            OrderDate = orderDate;

            this.items.UnionWith(orderItems);
        }

        public int Id { get; set; }

        public int CustomerId { get; }

        public string ShippingCity { get; }

        public string ShippingStreet { get; }

        public string ShippingHouseNumber { get; }

        public string ShippingZipCode { get; }

        public string PhoneNumber { get; }

        public decimal TotalAmount { get; }

        public decimal DiscountAmount { get; }

        public DateTime OrderDate { get; }

        public IReadOnlyCollection<OrderItemEntity> Items => items;
    }
}
