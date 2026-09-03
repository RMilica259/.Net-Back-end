using ECommerceApp.Application.UseCases.Queries.GetAllProducts;
using ECommerceApp.Application.UseCases.Queries.GetProductById;
using ECommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Infrastructure.Queries
{
    public class GetAllProductsQuery : IGetAllProductsQuery
    {
        private readonly AppDbContext _context;

        public GetAllProductsQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDto>> Execute()
        {
            return await _context.Products
                .Select(product => new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity
                })
                .ToListAsync();
        }
    }
}
