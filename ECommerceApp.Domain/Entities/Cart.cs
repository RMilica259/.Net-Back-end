using ECommerceApp.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Domain.Entities
{
    public class CartEntity
    {
        public CartEntity(int customerId)
        {
            CustomerId = customerId;
        }

        public int CustomerId { get; }
        public HashSet<CartItemEntity> Items = new ();

        public IReadOnlyCollection<CartItemEntity> items => Items;

        public void AddOrUpdateItem(CartItemEntity newItem)
        {
            var existingItem = Items.FirstOrDefault(i => i.ProductId == newItem.ProductId);

            if (existingItem is not null)
            {
                existingItem.IncreaseQuantity(newItem.Quantity);
            }
            else
            {
                Items.Add(newItem);
            }
        }


        public decimal Total() => Items.Sum(i => i.TotalPrice());
    }
}
