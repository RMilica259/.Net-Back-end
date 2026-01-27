using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Application.IRepository
{
    public interface ICustomerRepository
    {
        Task<bool> Exists (int id);
        Task<CustomerEntity?> GetById (int id);
        Task<CustomerEntity?> GetWithAddresses(int id);
    }
}
