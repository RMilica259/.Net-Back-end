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
        Task<CartEntity?> GetCartById(int customerId);
        Task CreateCart(int customerId);
        Task Add(CartItemEntity cartItem);
        Task Delete(int customerId);
        Task Save(CartEntity cart);
    }
}
