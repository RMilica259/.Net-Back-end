using Microsoft.EntityFrameworkCore;
using ECommerceApp.Models;
using ECommerceApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    public class OrderController : ControllerBase
    {
        public AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
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
            _context.Orders.Add(order);
            _context.SaveChanges();
            return Ok(order);
        }
    }
}
