namespace ECommerceApp.Infrastructure.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";

        public List<CustomerAddress> Addresses { get; set; } = new();

        public Cart? Cart { get; set; }
        public List<Order> Orders { get; set; } = new();

    }
}
