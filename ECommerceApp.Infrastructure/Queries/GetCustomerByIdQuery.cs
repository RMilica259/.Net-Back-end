using ECommerceApp.Application.UseCases.Queries.GetCustomerById;
using ECommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Infrastructure.Queries
{
    public class GetCustomerByIdQuery : IGetCustomerByIdQuery
    {
        private readonly AppDbContext _context;

        public GetCustomerByIdQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerDto?> Execute(int customerId)
        {
            return await _context.Customers
                .Where(customer => customer.Id == customerId)
                .Select(customer => new CustomerDto
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    PhoneNumber = customer.PhoneNumber
                })
                .SingleOrDefaultAsync();
        }
    }
}
