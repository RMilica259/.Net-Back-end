using ECommerceApp.Domain.OperationResult;
using MediatR;

namespace ECommerceApp.Application.UseCases.Commands.AddProductToCart
{
    public class AddProductToCartRequest : IRequest<Result>
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int CartId { get; set; }
        public int Quantity { get; set; }
    }
}
