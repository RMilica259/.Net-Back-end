using ECommerceApp.Application.IRepository;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                .Select(x => new CustomerEntity($"{x.FirstName} {x.LastName}", x.Email)
                { Id = x.Id})
                .SingleOrDefaultAsync();
        }

        public async Task<CustomerEntity?> GetWithAddresses(int id)
        {
            var customer = await _appDbContext.Customers
                .Include(c => c.Addresses)
                    .ThenInclude(ca => ca.Address)
                .SingleOrDefaultAsync(c => c.Id == id);

            if (customer is null)
                return null;

            var entity = new CustomerEntity(
                $"{customer.FirstName} {customer.LastName}",
                customer.Email)
            {
                Id = customer.Id
            };

            foreach (var ca in customer.Addresses)
            {
                entity.AddAddress(
                    new AddressEntity(
                        ca.Address.City,
                        ca.Address.Street,
                        ca.Address.HouseNumber,
                        ca.Address.ZipCode
                    )
                );
            }

            return entity;
        }
    }
}
