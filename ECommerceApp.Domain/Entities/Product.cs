namespace ECommerceApp.Domain.Entities
{
    public class ProductEntity
    {
        public ProductEntity(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
        public int Id { get; set; }
        public string Name { get; } 
        public decimal Price { get; }
    }
}
