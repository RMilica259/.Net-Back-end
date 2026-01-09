using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Application.UseCases.Queries.GetCartItem
{
    public class CartDto
    {
        public int CartId { get; set; }
        public int CustomerId { get; set; }
        public decimal Total { get; set; }
        public List<CartItemDto> Items { get; set; } = new();
    }
}
