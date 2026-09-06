using ECommerceApp.Application.UseCases.Commands.CreateOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Web.Controllers
{
    [ApiController]
    [Route("order")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            var result = await _mediator.Send(request);

            if (!result.IsSuccessful)
            {
                return BadRequest(
                    result.Error?.ErrorMessage ?? result.Message);
            }

            return Ok();
        }
    }
}
