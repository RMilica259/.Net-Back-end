using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Domain.Entities
{
    public class CartEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; private set; }
        public List<CartItemEntity> Items { get; set; } = new List<CartItemEntity>();

        public CartEntity(int customerId)
        {
            CustomerId = customerId;
        }
    }
}
