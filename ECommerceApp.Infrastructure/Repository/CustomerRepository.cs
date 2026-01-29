using ECommerceApp.Application.IRepository;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Infrastructure.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _appDbContext;
        public CustomerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> Exists(int id)
        {
            return await _appDbContext.Customers.AnyAsync(x => x.Id == id);
        }

        public async Task<CustomerEntity?> GetById(int id)
        {
            return await _appDbContext.Customers
                .Where(x => x.Id == id)
                .Select(x => new CustomerEntity($"{x.FirstName} {x.LastName}", x.Email,
                    x.Addresses.Select(c => new AddressEntity(
                        c.Address.City,
                        c.Address.Street,
                        c.Address.HouseNumber,
                        c.Address.ZipCode))
                    .ToHashSet()
                )
                {
                    Id = x.Id
                })
                .SingleOrDefaultAsync();
        }
    }
}
