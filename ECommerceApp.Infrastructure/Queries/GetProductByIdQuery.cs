using ECommerceApp.Application.UseCases.Queries.GetProductById;
using ECommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Infrastructure.Queries
{
    public class GetProductByIdQuery : IGetProductByIdQuery
    {
        private readonly AppDbContext _context;

        public GetProductByIdQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProductDto?> Execute(int productId)
        {
            return await _context.Products
                .Where(product => product.Id == productId)
                .Select(product => new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity
                })
                .SingleOrDefaultAsync();
        }
    }
}
