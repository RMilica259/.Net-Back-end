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
        private readonly IGetCartItemQuery _query;
        public GetCartItemHandler(IGetCartItemQuery query)
        {
            _query = query;
        }

        async Task<IEnumerable<CartItemDto>> IRequestHandler<GetCartItemRequest, IEnumerable<CartItemDto>>.Handle(GetCartItemRequest request, CancellationToken cancellationToken)
        {
            var cartItem = await _query.Execute(request.CustomerId);
            return cartItem;
        }
    }
}
