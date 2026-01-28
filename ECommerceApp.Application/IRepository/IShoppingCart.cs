using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Application.IRepository
{
    public interface IShoppingCartRepository
    {
        Task<CartEntity?> GetCartById(int customerId);
        Task<CartEntity> CreateCart(int customerId);
        Task Add(CartItemEntity cartItem);
        Task Delete(int customerId);
        Task Save(CartEntity cart);
    }
}
