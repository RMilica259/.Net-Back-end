using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceApp.Infrastructure.Data;
using ECommerceApp.Domain.IRepository;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Infrastructure.Models;

namespace ECommerceApp.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task PlaceOrder(OrderEntity orderEntity)
        {
            var order = new Order
            {
                CustomerId = orderEntity.CustomerId,
                City = orderEntity.City,
                Street = orderEntity.Street,
                HouseNumber = orderEntity.HouseNumber,
                ZipCode = orderEntity.ZipCode,
                PhoneNumber = orderEntity.PhoneNumber,
                TotalAmount = 0,
                DiscountAmount = 0 
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }
    }
}
