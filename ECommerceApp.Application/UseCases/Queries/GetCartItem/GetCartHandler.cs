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
    public class GetCartHandler : IRequestHandler<GetCartRequest, CartDto?>
    {
        private readonly IGetCartQuery _query;

        public GetCartHandler(IGetCartQuery query)
        {
            _query = query;
        }

        public async Task<CartDto?> Handle(GetCartRequest request, CancellationToken cancellationToken)
        {
            return await _query.Execute(request.CustomerId);
        }
    }
}
