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
            return await _appDbContext.Customers
                .AnyAsync(x => x.Id == id);
        }

        public async Task<CustomerEntity?> GetById(int id)
        {
            var customer = await _appDbContext.Customers
                .Include(x => x.Addresses)
                .SingleOrDefaultAsync(x => x.Id == id);

            if (customer == null)
            {
                return null;
            }

            var addresses = customer.Addresses
                .Select(address => new CustomerAddressEntity(
                    address.City,
                    address.Street,
                    address.HouseNumber,
                    address.ZipCode,
                    address.IsDefault)
                {
                    Id = address.Id
                })
                .ToHashSet();

            return new CustomerEntity(
                $"{customer.FirstName} {customer.LastName}",
                customer.Email,
                addresses)
            {
                Id = customer.Id
            };
        }
    }
}
