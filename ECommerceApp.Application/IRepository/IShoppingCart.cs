using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Application.IRepository
{
    public interface IShoppingCartRepository
    {
        Task AddToCart(CartItemEntity cart);
        Task<CartItemEntity?> GetById(int id);
        Task<IEnumerable<CartItemEntity>> GetAll(int customerId);
        Task ClearCart(int customerId);
    }
}
