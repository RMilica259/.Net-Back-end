using ECommerceApp.Application.IRepository;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.ValueObjects;
using ECommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProductEntity?> GetById(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return null;

            return new ProductEntity(
               product.Name,
               product.Price,
               Quantity.FromInt(product.Quantity)
            );
        }
    }
}
