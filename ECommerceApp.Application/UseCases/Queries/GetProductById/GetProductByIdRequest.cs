using MediatR;

namespace ECommerceApp.Application.UseCases.Queries.GetProductById
{
    public class GetProductByIdRequest : IRequest<ProductDto?>
    {
        public int ProductId { get; set; }
    }
}
