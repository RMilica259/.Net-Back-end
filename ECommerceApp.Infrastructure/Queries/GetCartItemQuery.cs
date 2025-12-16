using ECommerceApp.Application.UseCases.Queries.GetCartItem;
using ECommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Infrastructure.Queries
{
    public class GetCartItemQuery : IGetCartItemQuery
    {
        private readonly AppDbContext _context;
        public GetCartItemQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CartItemDto>> Execute(int id)
        {
            return await _context.CartItems
                .Where(x => x.CustomerId == id)
                .Select(x => new CartItemDto()
                {
                    ProductId = x.ProductId,
                    ProductName = x.Product.Name,
                    Price = x.Product.Price,
                    Quantity = x.Quantity
                }).ToListAsync();
        }
    }
}
