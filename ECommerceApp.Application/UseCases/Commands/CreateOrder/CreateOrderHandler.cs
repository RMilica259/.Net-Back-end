using ECommerceApp.Application.IRepository;
using ECommerceApp.Domain.Date;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Errors;
using ECommerceApp.Domain.OperationResult;
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
        private readonly IProductRepository _productRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateOrderHandler(IShoppingCartRepository shoppingCartRepository, IOrderRepository orderRepository, IProductRepository productRepository, IDateTimeProvider dateTimeProvider)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Result> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var cartItems = await _shoppingCartRepository.GetById(request.CustomerId);

            if (cartItems == null) return null;

            decimal totalAmount = 0;
            decimal discountAmount = 0;

            var address = new Address(
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

            await _shoppingCartRepository.Clear(request.CustomerId);

            return Result.Success();
        }
    }
}
