namespace ECommerceApp.Infrastructure.Models
{
    public class CustomerAddress
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; } = null!;

        public string City { get; set; } = string.Empty;

        public string Street { get; set; } = string.Empty;

        public string HouseNumber { get; set; } = string.Empty;

        public string ZipCode { get; set; } = string.Empty;

        public bool IsDefault { get; set; }
    }
}
