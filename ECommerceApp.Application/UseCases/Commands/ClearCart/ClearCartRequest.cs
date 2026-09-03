using ECommerceApp.Domain.OperationResult;
using MediatR;

namespace ECommerceApp.Application.UseCases.Commands.ClearCart
{
    public class ClearCartRequest : IRequest<Result>
    {
        public int CustomerId { get; set; }
    }
}
