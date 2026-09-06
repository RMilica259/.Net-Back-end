using ECommerceApp.Application.IRepository;
using ECommerceApp.Application.Services;
using ECommerceApp.Domain.Date;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Errors;
using ECommerceApp.Domain.OperationResult;
using MediatR;

namespace ECommerceApp.Application.UseCases.Commands.CreateOrder
{
    public class CreateOrderHandler
        : IRequestHandler<CreateOrderRequest, Result>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly Discount _discount;

        public CreateOrderHandler(
            IShoppingCartRepository shoppingCartRepository,
            IOrderRepository orderRepository,
            IDateTimeProvider dateTimeProvider,
            Discount discount)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _orderRepository = orderRepository;
            _dateTimeProvider = dateTimeProvider;
            _discount = discount;
        }

        public async Task<Result> Handle(
            CreateOrderRequest request,
            CancellationToken cancellationToken)
        {
            var cart = await _shoppingCartRepository
                .GetById(request.CustomerId);

            if (cart == null)
            {
                return Result.Failure(
                    "Shopping cart not found for this customer.");
            }

            if (cart.Items.Count == 0)
            {
                return Result.Failure(
                    DomainErrors.Cart.CartIsEmpty());
            }

            var totalAmount = cart.Total();
            var orderDate = _dateTimeProvider.UtcNow();

            var discountAmount = _discount.Calculate(
                totalAmount,
                request.PhoneNumber,
                orderDate);

            var orderItems = cart.Items
                .Select(item => new OrderItemEntity(
                    item.ProductId,
                    item.Price,
                    item.Quantity))
                .ToList();

            var order = new OrderEntity(
                request.CustomerId,
                request.ShippingAddress.City,
                request.ShippingAddress.Street,
                request.ShippingAddress.HouseNumber,
                request.ShippingAddress.ZipCode,
                request.PhoneNumber,
                totalAmount,
                discountAmount,
                orderDate,
                orderItems);

            await _orderRepository.Create(order);

            await _shoppingCartRepository.Delete(
                request.CustomerId);

            return Result.Success();
        }
    }
}
