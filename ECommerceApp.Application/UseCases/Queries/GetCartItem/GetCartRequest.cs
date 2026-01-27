using MediatR;

namespace ECommerceApp.Application.UseCases.Queries.GetCartItem
{
    public class GetCartRequest : IRequest<CartDto?>
    {
        public int CustomerId { get; set; }
    }
}
