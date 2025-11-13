namespace ECommerceApp.Infrastructure.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string City { get; set; } = "";
        public string Street { get; set; } = "";
        public string HouseNumber { get; set; } = "";
        public string ZipCode { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

    }
}
