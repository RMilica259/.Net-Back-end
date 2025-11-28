using Microsoft.EntityFrameworkCore;
using ECommerceApp.Infrastructure.Models;
using ECommerceApp.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using ECommerceApp.Application.IRepository;
using ECommerceApp.Domain.Entities;
using MediatR;

namespace ECommerceApp.Web.Controllers
{
    [Route("cart")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ShoppingCartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(CartItemEntity cart)
        {
            await _mediator.Send(Request);
            return Ok();
        }
    }
}
