using Microsoft.AspNetCore.Mvc;
using MediatR;
using ECommerceApp.Application.UseCases.Queries.GetCartItem;
using ECommerceApp.Application.UseCases.Commands.AddProductToCart;

namespace ECommerceApp.Web.Controllers
{
    [ApiController]
    [Route("cart")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ShoppingCartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(AddProductToCartRequest request)
        { 
            var result = await _mediator.Send(request);

            if (!result.IsSuccessful) return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCartItems(int customerId)
        {
            var request = new GetCartRequest { CustomerId = customerId };

            var result = await _mediator.Send(request);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

    }
}
