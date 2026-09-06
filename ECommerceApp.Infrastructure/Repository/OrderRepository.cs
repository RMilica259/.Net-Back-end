using ECommerceApp.Application.IRepository;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Infrastructure.Data;
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

        public async Task Create(OrderEntity orderEntity)
        {
            var order = new Order
            {
                CustomerId = orderEntity.CustomerId,
                ShippingCity = orderEntity.ShippingCity,
                ShippingStreet = orderEntity.ShippingStreet,
                ShippingHouseNumber = orderEntity.ShippingHouseNumber,
                ShippingZipCode = orderEntity.ShippingZipCode,
                PhoneNumber = orderEntity.PhoneNumber,
                TotalAmount = orderEntity.TotalAmount,
                DiscountAmount = orderEntity.DiscountAmount,
                OrderDate = orderEntity.OrderDate,

                Items = orderEntity.Items
                    .Select(item => new OrderItem
                    {
                        ProductId = item.ProductId,
                        UnitPrice = item.UnitPrice,
                        Quantity = item.Quantity.Value
                    })
                    .ToList()
            };

            _context.Orders.Add(order);

            await _context.SaveChangesAsync();

            orderEntity.Id = order.Id;
        }
    }
}
