using Microsoft.EntityFrameworkCore;
using ECommerceApp.Models;
using ECommerceApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    public class ShoppingCartController : ControllerBase
    {
        public AppDbContext _context;

        public ShoppingCartController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult AddToCart(int customerId, int productId, int quantity)
        {
            var cartItem = new CartItem
            {
                CustomerId = customerId,
                ProductId = productId,
                Quantity = quantity
            };
            _context.CartItems.Add(cartItem);
            _context.SaveChanges();
            return Ok(cartItem);
        }
    }
}
