using Microsoft.EntityFrameworkCore;
using ECommerceApp.Infrastructure.Models;
using ECommerceApp.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using ECommerceApp.Domain.IRepository;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Web.Controllers
{
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<IActionResult> PlaceOrder(OrderEntity order)
        {
            await _orderRepository.PlaceOrder(order);
            return Ok();
        }
    }
}
