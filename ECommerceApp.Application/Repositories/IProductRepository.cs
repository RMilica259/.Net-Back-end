using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Application.IRepository
{
    public interface IProductRepository
    {
        Task<ProductEntity?> GetById(int id);
    }
}
