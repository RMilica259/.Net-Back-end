using ECommerceApp.Domain.Entities;
using ECommerceApp.Application.IRepository;
using ECommerceApp.Infrastructure.Data;
using ECommerceApp.Infrastructure.Models;
using ECommerceApp.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Infrastructure.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly AppDbContext _context;
        public ShoppingCartRepository(AppDbContext context)
        {
            _context = context;
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

        public async Task Delete(int customerId)
        {
            var cartItems = await _context.CartItems.Where(x => x.Cart.CustomerId == customerId).ToListAsync();

            if (cartItems.Count > 0)
            {
                _context.CartItems.RemoveRange(cartItems);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<CartEntity> Create(CartEntity cartEntity)
        {
            var cart = new Cart
            {
                CustomerId = cartEntity.CustomerId
            };

            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();

            cartEntity.Id = cart.Id;

            return cartEntity;
        }

        public async Task<CartEntity?> GetById(int customerId)
        {
            return await _context.Carts
                .Where(x => x.CustomerId == customerId)
                .Select(x => new CartEntity(customerId)
                {
                    Id = x.Id,
                    Items = x.Items.Select(ci => new CartItemEntity(
                        ci.ProductId,
                        ci.Price,
                        Quantity.FromInt(ci.Quantity))
                    {
                        Id = ci.Id
                    }
                    ).ToHashSet()
                })
                .SingleOrDefaultAsync();
        }

        public async Task Update(CartEntity cart)
        {
            var dbCart = await _context.Carts
                .Include(c => c.Items)
                .SingleAsync(c => c.CustomerId == cart.CustomerId);

            cart.Items.ToList().ForEach(item =>
            {
                var existing = dbCart.Items.SingleOrDefault(i => i.ProductId == item.ProductId);

                if (existing is null)
                {
                    dbCart.Items.Add(new CartItem
                    {
                        ProductId = item.ProductId,
                        Price = item.Price,
                        Quantity = item.Quantity.Value
                    });
                }
                else existing.Quantity = item.Quantity.Value;
            });
            await _context.SaveChangesAsync();
        }
    }
}
