using MediatR;

namespace ECommerceApp.Application.UseCases.Queries.GetProductById
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdRequest, ProductDto?>
    {
        private readonly IGetProductByIdQuery _query;

        public GetProductByIdHandler(IGetProductByIdQuery query)
        {
            _query = query;
        }

        public async Task<ProductDto?> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
        {
            return await _query.Execute(request.ProductId);
        }
    }
}
