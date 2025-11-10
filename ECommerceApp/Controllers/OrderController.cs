using Microsoft.EntityFrameworkCore;
using ECommerceApp.Models;
using ECommerceApp.Data;
using Microsoft.AspNetCore.Mvc;
using ECommerceApp.Repository.IRepository;

namespace ECommerceApp.Controllers
{
    public class OrderController : ControllerBase
    {
        private readonly IRepository<Order> _orderRepository;

        public OrderController(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;  //DI
        }
        
        public IActionResult PlaceOrder(int customerId, string city, string street, string houseNumber, string zipCode, string phoneNumber)
        {
          
            var order = new Order
            {
                CustomerId = customerId,
                City = city,
                Street = street,
                HouseNumber = houseNumber,
                ZipCode = zipCode,
                PhoneNumber = phoneNumber,
                TotalAmount = 0,
                DiscountAmount = 0 
            };
            _orderRepository.Add(order);
            _orderRepository.SaveChanges();
            return Ok(order);
        }
    }
}
