using ECommerceApp.Domain.Entities;
using ECommerceApp.Application.IRepository;
using ECommerceApp.Infrastructure.Data;
using ECommerceApp.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceApp.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Infrastructure.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly AppDbContext _context;
        private readonly IProductRepository _productRepository;
        public ShoppingCartRepository(AppDbContext context, IProductRepository productRepository)
        {
            _context = context;
            _productRepository = productRepository;
        }

        public async Task Add(CartItemEntity ci)
        {
            var cartItem = new CartItem
            {
                ProductId = ci.ProductId,
                Price = ci.Price,
                Quantity = ci.Quantity.Value
            };
            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task Clear(int customerId)
        {
            var cartItems = await _context.CartItems.Where(x => x.Cart.CustomerId == customerId).ToListAsync();

            if (cartItems.Count > 0)
            {
                _context.CartItems.RemoveRange(cartItems);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CreateCart(int customerId)
        {
            var cart = new Cart
            {
                CustomerId = customerId,
            };

            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        public async Task<CartEntity?> GetCartById(int customerId)
        {
            return await _context.Carts
                .Where(x => x.CustomerId == customerId)
                .Select(x => new CartEntity(customerId)
                {
                    Items = x.Items.Select(ci => new CartItemEntity(
                        ci.ProductId,
                        ci.Price,
                        Quantity.FromInt(ci.Quantity)) 
                    ).ToList()
                })
                .SingleOrDefaultAsync();
        }
    }
}
