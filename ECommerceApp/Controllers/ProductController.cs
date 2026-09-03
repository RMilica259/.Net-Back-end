using ECommerceApp.Application.UseCases.Queries.GetAllProducts;
using ECommerceApp.Application.UseCases.Queries.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Web.Controllers
{
    [ApiController]
    [Route("product")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var request = new GetProductByIdRequest
            {
                ProductId = id
            };

            var result = await _mediator.Send(request);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _mediator.Send(new GetAllProductsRequest());

            return Ok(result);
        }
    }
}
