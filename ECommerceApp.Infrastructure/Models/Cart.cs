namespace ECommerceApp.Infrastructure.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public decimal Total { get; set; }
        public List<CartItem> Items { get; set;} = new List<CartItem>();
    }
}
