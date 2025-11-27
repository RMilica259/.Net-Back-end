using ECommerceApp.Domain.Entities;
using ECommerceApp.Application.IRepository;
using ECommerceApp.Infrastructure.Data;
using ECommerceApp.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Infrastructure.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly AppDbContext _context;
        public ShoppingCartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddToCart(CartItemEntity cart)
        {
            var cartItem = new CartItem
            {
                CustomerId = cart.CustomerId,
                ProductId = cart.ProductId,
                Quantity = cart.Quantity.Value
            };
            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
        }
    }
}
