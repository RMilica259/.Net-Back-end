using ECommerceApp.Application.UseCases.Queries.GetCartItem;
using ECommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Infrastructure.Queries
{
    public class GetCartQuery : IGetCartQuery
    {
        private readonly AppDbContext _context;
        public GetCartQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CartDto?> Execute(int customerId)
        {
            return await _context.Carts
                .Where(x => x.CustomerId == customerId)
                .Select(x => new CartDto()
                {
                    CartId = x.Id,
                    CustomerId = x.CustomerId,
                    Total = x.Total,
                    Items = x.Items.Select(ci => new CartItemDto
                    {
                        ProductId = ci.ProductId,
                        ProductName = ci.Product.Name,
                        Price = ci.Product.Price,
                        Quantity = ci.Quantity
                    }).ToList()
                })
        .SingleOrDefaultAsync();
        }
    }
}
