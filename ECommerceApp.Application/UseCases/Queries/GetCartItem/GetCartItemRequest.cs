using ECommerceApp.Domain.OperationResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Application.UseCases.Queries.GetCartItem
{
    public class GetCartItemRequest : IRequest<IEnumerable<CartItemDto>>
    {
        public int CustomerId { get; set; }
    }
}
