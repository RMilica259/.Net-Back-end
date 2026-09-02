using ECommerceApp.Domain.Errors;
using ECommerceApp.Domain.OperationResult;

namespace ECommerceApp.Domain.Entities
{
    public class CartEntity
    {
        public CartEntity(int customerId)
        {
            CustomerId = customerId;
        }

        public int Id { get; set; }
        public int CustomerId { get; }
        private readonly HashSet<CartItemEntity> items = new();

        public IReadOnlyCollection<CartItemEntity> Items => items;

        public Result AddItem(CartItemEntity item)
        {
            if (items.Any(i => i.ProductId == item.ProductId))
                return Result.Failure(DomainErrors.Cart.ItemAlreadyExists());

            items.Add(item);
            return Result.Success();
        }

        public Result UpdateItemQuantity(CartItemEntity cartItem)
        {
            var item = items.FirstOrDefault(i => i.ProductId == cartItem.ProductId);

            if (item is null)
            {
                return Result.Failure(DomainErrors.Cart.ItemNotFound());
            }

            item.IncreaseQuantity(cartItem.Quantity);
            return Result.Success();
        }

        public decimal Total() => items.Sum(i => i.TotalPrice());
    }
}
