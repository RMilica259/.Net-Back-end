using ECommerceApp.Application.IRepository;
using ECommerceApp.Application.Services;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Errors;
using ECommerceApp.Domain.OperationResult;
using ECommerceApp.Domain.ValueObjects;
using MediatR;

namespace ECommerceApp.Application.UseCases.Commands.AddProductToCart
{
    public class AddProductToCartHandler : IRequestHandler<AddProductToCartRequest, Result>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IProductRepository _productRepository;
        private readonly StockAvailability _stockAvailability;
        public AddProductToCartHandler(IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository, StockAvailability stockAvailability)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _productRepository = productRepository;
            _stockAvailability = stockAvailability;
        }

        public async Task<Result> Handle(AddProductToCartRequest request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(request.ProductId);
            if (product == null) 
                return Result.Failure("Product not found");

            var cart = await _shoppingCartRepository.GetById(request.CustomerId);

            if (cart == null)
            {
                cart = new CartEntity(request.CustomerId);
                cart = await _shoppingCartRepository.Create(cart);
            }

            var existingItem = cart.items.FirstOrDefault(i => i.ProductId == request.ProductId);

            int totalQuantity;

            if (existingItem == null) totalQuantity = request.Quantity;
            else totalQuantity = existingItem.Quantity.Value + request.Quantity;

            if (!await _stockAvailability.IsQuantityAvailable(product.Quantity.Value, request.ProductId, totalQuantity))
                return Result.Failure("Not enough stock available");

            var cartItem = new CartItemEntity(request.ProductId, product.Price, Quantity.FromInt(request.Quantity));

            var result = cart.AddItem(cartItem);

            if (!result.IsSuccessful)
            {
                if (result.Error != DomainErrors.Cart.ItemAlreadyExists())
                    return result;

                result = cart.UpdateItemQuantity(cartItem);
                if (!result.IsSuccessful)
                    return result;
            }

            await _shoppingCartRepository.Update(cart);
            return Result.Success();
        }
    }
}
