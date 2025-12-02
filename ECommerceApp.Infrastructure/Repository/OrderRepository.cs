using ECommerceApp.Infrastructure.Data;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Infrastructure.Models;
using ECommerceApp.Application.IRepository;

namespace ECommerceApp.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(OrderEntity orderEntity)
        {
            var order = new Order
            {
                CustomerId = orderEntity.CustomerId,
                City = orderEntity.ShippingAddress.City,
                Street = orderEntity.ShippingAddress.Street,
                HouseNumber = orderEntity.ShippingAddress.HouseNumber,
                ZipCode = orderEntity.ShippingAddress.ZipCode,
                PhoneNumber = orderEntity.PhoneNumber,
                TotalAmount = 0,
                DiscountAmount = 0 
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }
    }
}
