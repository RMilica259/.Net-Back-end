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

        public int Id { get; set; }
        public int CustomerId { get; private set; }
        public decimal Total { get; private set; }
        public List<CartItemEntity> Items { get; set; } = new List<CartItemEntity>();

        public void RecalculateTotal()
        {
            Total = Items.Sum(i => i.Product.Price * i.Quantity.Value);
        }
    }
}
