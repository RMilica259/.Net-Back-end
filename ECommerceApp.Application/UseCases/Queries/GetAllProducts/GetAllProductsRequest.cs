using ECommerceApp.Application.UseCases.Queries.GetProductById;
using MediatR;

namespace ECommerceApp.Application.UseCases.Queries.GetAllProducts
{
    public class GetAllProductsRequest : IRequest<List<ProductDto>>
    {
    }
}
