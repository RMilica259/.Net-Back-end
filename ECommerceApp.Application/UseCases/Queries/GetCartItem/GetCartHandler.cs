using MediatR;

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
