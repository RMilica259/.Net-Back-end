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
        public List<CartItemEntity> Items = new ();

        public IReadOnlyCollection<CartItemEntity> items => Items;

        public void AddItem(CartItemEntity cart)
        {
            Items.Add(cart);
        }

        public decimal Total() => Items.Sum(i => i.TotalPrice());
    }
}
