using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Application.IRepository
{
    public interface IShoppingCartRepository
    {
        Task<CartEntity?> GetById(int customerId);
        Task<CartEntity> Create(CartEntity cart);
        Task Add(CartItemEntity cartItem);
        Task Delete(int customerId);
        Task Update(CartEntity cart);
    }
}
