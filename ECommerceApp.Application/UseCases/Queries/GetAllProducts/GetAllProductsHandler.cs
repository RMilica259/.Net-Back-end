using ECommerceApp.Application.UseCases.Queries.GetProductById;
using MediatR;

namespace ECommerceApp.Application.UseCases.Queries.GetAllProducts
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsRequest, List<ProductDto>>
    {
        private readonly IGetAllProductsQuery _query;

        public GetAllProductsHandler(IGetAllProductsQuery query)
        {
            _query = query;
        }

        public async Task<List<ProductDto>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
        {
            return await _query.Execute();
        }
    }
}
