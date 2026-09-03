using ECommerceApp.Application.IRepository;
using ECommerceApp.Domain.OperationResult;
using MediatR;

namespace ECommerceApp.Application.UseCases.Commands.ClearCart
{
    public class ClearCartHandler : IRequestHandler<ClearCartRequest, Result>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ClearCartHandler(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<Result> Handle(ClearCartRequest request, CancellationToken cancellationToken)
        {
            var cart = await _shoppingCartRepository.GetById(request.CustomerId);

            if (cart is null)
                return Result.Failure("Shopping cart not found for this customer.");

            await _shoppingCartRepository.Delete(request.CustomerId);

            return Result.Success();
        }
    }
}
