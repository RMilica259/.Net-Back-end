using ECommerceApp.Application.UseCases.Queries.GetCustomerById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Web.Controllers
{
    [ApiController]
    [Route("customer")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var request = new GetCustomerByIdRequest
            {
                CustomerId = id
            };

            var result = await _mediator.Send(request);

            if (result is null)
                return NotFound();

            return Ok(result);
        }
    }
}
