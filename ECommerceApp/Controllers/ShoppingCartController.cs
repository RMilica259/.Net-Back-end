using Microsoft.EntityFrameworkCore;
using ECommerceApp.Infrastructure.Models;
using ECommerceApp.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using ECommerceApp.Application.IRepository;
using ECommerceApp.Domain.Entities;
using MediatR;
using ECommerceApp.Application.UseCases.Queries.GetCartItem;

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

        [HttpGet]
        public async Task<IActionResult> GetCartItems(int customerId)
        {
            var request = new GetCartItemRequest { CustomerId = customerId };

            var result = await _mediator.Send(request);

            return Ok(result);
        }

    }
}
