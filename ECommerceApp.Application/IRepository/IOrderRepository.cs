using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Application.IRepository
{
    public interface IOrderRepository
    {
        Task Create(OrderEntity order);
    }
}
