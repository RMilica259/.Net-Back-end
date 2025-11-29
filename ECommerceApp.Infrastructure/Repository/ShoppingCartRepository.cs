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

        public async Task ClearCart(int customerId)
        {
            var cartItems = await _context.CartItems.Where(x => x.CustomerId == customerId).ToListAsync();

            if (cartItems.Count > 0)
            {
                _context.CartItems.RemoveRange(cartItems);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<CartItemEntity?> GetById(int id)
        {
            var cartItem = _context.CartItems.FirstOrDefault(x => x.Id == id);

            if (cartItem == null) return null;

            var product = await _productRepository.GetById(cartItem.ProductId);

            if (product == null) return null;

            var quantity = new Quantity(cartItem.Quantity);

            return new CartItemEntity(
                cartItem.CustomerId,
                cartItem.ProductId,
                quantity,
                product
            )
            { 
                Id  = cartItem.Id 
            };
        }
    }
}
