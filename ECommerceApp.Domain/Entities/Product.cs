namespace ECommerceApp.Domain.Entities
{
    public class ProductEntity
    {
        public ProductEntity(string name, decimal price, int localPrice)
        {
            Name = name;
            Price = price;
            LocalPrice = localPrice;
        }
        public int Id { get; set; }
        public string Name { get; } 
        public decimal Price { get; }
        public int LocalPrice { get; }
    }
}
