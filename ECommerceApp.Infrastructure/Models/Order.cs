namespace ECommerceApp.Infrastructure.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Address? Address { get; set; } 
        public string PhoneNumber { get; set; } = "";
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}
