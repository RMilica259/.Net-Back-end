using ECommerceApp.Application.IRepository;
using ECommerceApp.Domain.Date;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Errors;
using ECommerceApp.Domain.OperationResult;
using ECommerceApp.Application.Services;
using ECommerceApp.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Application.UseCases.Commands.CreateOrder
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderRequest, Result>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly Discount _discount;

        public CreateOrderHandler(IShoppingCartRepository shoppingCartRepository, IOrderRepository orderRepository, IDateTimeProvider dateTimeProvider, Discount discount)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _orderRepository = orderRepository;
            _dateTimeProvider = dateTimeProvider;
            _discount = discount;
        }

        public async Task<Result> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var cart = await _shoppingCartRepository.GetCartById(request.CustomerId);

            if (cart == null)
                return Result.Failure("Shopping cart not found for this customer.");


            decimal totalAmount = cart.Items.Sum(x => x.TotalPrice());

            var discountAmount = _discount.Calculate(totalAmount, request.PhoneNumber, _dateTimeProvider.UtcNow());

            var address = new AddressEntity(
                request.ShippingAddress.City,
                request.ShippingAddress.Street,
                request.ShippingAddress.HouseNumber,
                request.ShippingAddress.ZipCode
            );

            var order = new OrderEntity(
                request.CustomerId,
                address,
                request.PhoneNumber,
                totalAmount,
                discountAmount,
                _dateTimeProvider.UtcNow() 
            );

            await _orderRepository.Create(order);

            await _shoppingCartRepository.Delete(request.CustomerId);

            return Result.Success();
        }
    }
}
