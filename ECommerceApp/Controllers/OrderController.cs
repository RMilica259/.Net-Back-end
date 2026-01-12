using Microsoft.EntityFrameworkCore;
using ECommerceApp.Infrastructure.Models;
using ECommerceApp.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using ECommerceApp.Application.IRepository;
using ECommerceApp.Domain.Entities;
using MediatR;
using ECommerceApp.Application.UseCases.Commands.CreateOrder;

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

            if(!result.IsSuccessful) return BadRequest(result.Message);

            return Ok();
        }
    }
}
