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

        public async Task Add(CartItemEntity cart)
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

        public async Task Clear(int customerId)
        {
            var cartItems = await _context.CartItems.Where(x => x.CustomerId == customerId).ToListAsync();

            if (cartItems.Count > 0)
            {
                _context.CartItems.RemoveRange(cartItems);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CreateCart(CartEntity cart)
        {
            var c = new Cart
            {
                CustomerId = cart.CustomerId,
                Items = cart.Items.Select(x => new CartItem
                {
                    CustomerId = x.CustomerId,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity.Value
                }).ToList()
            };

            _context.Carts.Add(c);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CartItemEntity>> GetAll(int customerId)
        {
            var cartItems = await _context.CartItems
                .Where(x => x.CustomerId == customerId)
                .ToListAsync();

            var result = new List<CartItemEntity>();

            foreach (var item in cartItems)
            {
                var product = await _productRepository.GetById(item.ProductId);

                if (product == null)
                    continue;

                var quantity = new Quantity(item.Quantity);

                var entity = new CartItemEntity(
                    item.CustomerId,
                    item.ProductId,
                    quantity,
                    product
                )
                {
                    Id = item.Id
                };

                result.Add(entity);
            }

            return result;
        }


        public async Task<CartItemEntity?> GetById(int id)
        {
            return await _context.CartItems
                .Select(x => new CartItemEntity(
                    x.CustomerId, 
                    x.ProductId, 
                    new Quantity(x.Quantity),
                    new ProductEntity(x.Product.Name, x.Product.Price, new Quantity(x.Product.Quantity)) { Id = x.Product.Id })
                    { Id = x.Id }
                )
                .SingleOrDefaultAsync(x => x.Id == id);  
        }

        public async Task<CartEntity?> GetCartById(int customerId)
        {
            return await _context.Carts
                .Select(x => new CartEntity(
                    x.CustomerId
                )
                {
                    Id = x.Id,
                    Items = x.Items.Select(ci => new CartItemEntity(
                        ci.CustomerId,
                        ci.ProductId,
                        new Quantity(ci.Quantity),
                        new ProductEntity(ci.Product.Name, ci.Product.Price, new Quantity(ci.Product.Quantity)) { Id = ci.Product.Id })
                    { Id = ci.Id }
                    ).ToList()
                })
                .SingleOrDefaultAsync (x => x.CustomerId == customerId);
        }
    }
}
