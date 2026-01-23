using ECommerceApp.Application.IRepository;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Application.IServices;
using ECommerceApp.Domain.OperationResult;
using ECommerceApp.Application.Services;
using ECommerceApp.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            if (product == null) return Result.Failure("Product not found");

            var isAvailable = await _stockAvailability.IsQuantityAvailable(product.Quantity.Value, request.ProductId, request.Quantity);

            if (!isAvailable) return Result.Failure("Not enough stock available");

            var quantity = Quantity.FromInt(request.Quantity);

            var cart = await _shoppingCartRepository.GetCartById(request.CustomerId);

            if(cart == null)
            {
                await _shoppingCartRepository.CreateCart(request.CustomerId);
                cart = await _shoppingCartRepository.GetCartById(request.CustomerId);
            }

            cart.AddOrUpdateItem(
                new CartItemEntity(
                    request.ProductId,
                    product.Price,
                    quantity)
                );

            await _shoppingCartRepository.Save(cart);

            return Result.Success();
        }
    }
}
