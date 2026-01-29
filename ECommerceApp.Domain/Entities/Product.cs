using ECommerceApp.Domain.ValueObjects;

namespace ECommerceApp.Domain.Entities
{
    public class ProductEntity
    {
        public ProductEntity(string name, decimal price, Quantity quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public int Id { get; set; }
        public string Name { get; } 
        public decimal Price { get; }
        public Quantity Quantity { get; }
    }
}
