using ECommerceApp.Application.IRepository;
using ECommerceApp.Domain.OperationResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Application.UseCases.Queries.GetCartItem
{
    public class GetCartItemHandler : IRequestHandler<GetCartItemRequest, IEnumerable<CartItemDto>>
    {
        IShoppingCartRepository _shoppingCartRepository;
        public GetCartItemHandler(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        async Task<IEnumerable<CartItemDto>> IRequestHandler<GetCartItemRequest, IEnumerable<CartItemDto>>.Handle(GetCartItemRequest request, CancellationToken cancellationToken)
        {
            var cartItem = await _shoppingCartRepository.GetAll(request.CustomerId);

            var result = cartItem.Select(cItem => new CartItemDto
            {
                ProductId = cItem.ProductId,
                ProductName = cItem.Product.Name,
                Price = cItem.Product.Price,
                Quantity = cItem.Quantity.Value
            });
            return result;
        }
    }
}
