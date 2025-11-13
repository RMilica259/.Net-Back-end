using Microsoft.EntityFrameworkCore;
using ECommerceApp.Infrastructure.Models;
using ECommerceApp.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using ECommerceApp.Domain.IRepository;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Web.Controllers
{
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }
        public async Task<IActionResult> AddToCart(CartItemEntity cart)
        {
            await _shoppingCartRepository.AddToCart(cart);
            return Ok();
        }
    }
}
