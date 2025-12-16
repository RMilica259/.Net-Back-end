using ECommerceApp.Application.IRepository;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.OperationResult;
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
        public AddProductToCartHandler(IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(AddProductToCartRequest request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(request.ProductId);

            if (product == null) return Result.Failure("Product not found");

            var quantity = new Quantity(request.Quantity);

            var cartItem = new CartItemEntity
            (
               request.CustomerId,
               request.ProductId,
               request.CartId,
               quantity,
               product
            );

            await _shoppingCartRepository.Add(cartItem);

            return Result.Success();
        }
    }
}
