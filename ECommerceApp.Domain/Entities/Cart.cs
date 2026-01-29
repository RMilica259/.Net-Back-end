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
        public HashSet<CartItemEntity> Items = new ();

        public IReadOnlyCollection<CartItemEntity> items => Items;

        public Result AddItem(CartItemEntity item)
        {
            if (Items.Any(i => i.ProductId == item.ProductId))
                return Result.Failure(DomainErrors.Cart.ItemAlreadyExists());

            Items.Add(item);
            return Result.Success();
        }

        public Result UpdateItemQuantity(CartItemEntity cartItem)
        {
            var item = Items.FirstOrDefault(i => i.ProductId == cartItem.ProductId);

            if(item is null)
            {
                return Result.Failure(DomainErrors.Cart.ItemNotFound());
            }

            item.IncreaseQuantity(cartItem.Quantity);
            return Result.Success();
        }

        public decimal Total() => Items.Sum(i => i.TotalPrice());
    }
}
