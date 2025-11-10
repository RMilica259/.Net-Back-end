using Microsoft.EntityFrameworkCore;
using ECommerceApp.Models;
using ECommerceApp.Data;
using Microsoft.AspNetCore.Mvc;
using ECommerceApp.Repository.IRepository;

namespace ECommerceApp.Controllers
{
    public class ShoppingCartController : ControllerBase
    {
        private readonly IRepository<CartItem> _cartItemRepository;

        public ShoppingCartController(IRepository<CartItem> cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        public IActionResult AddToCart(int customerId, int productId, int quantity)
        {
            var cartItem = new CartItem
            {
                CustomerId = customerId,
                ProductId = productId,
                Quantity = quantity
            };
            _cartItemRepository.Add(cartItem);
            _cartItemRepository.SaveChanges();
            return Ok(cartItem);
        }
    }
}
