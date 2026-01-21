using AutoFixture.Xunit2;
using ECommerceApp.Application.IRepository;
using ECommerceApp.Application.UseCases.Commands.CreateOrder;
using ECommerceApp.Domain.Date;
using ECommerceApp.Domain.Entities;
using Moq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Application.UnitTests.UseCases.CreateOrder
{
    public class CreateOrderHandlerTests
    {
        [Theory]
        [AutoMoqInlineData]
        public async Task Handle_ValidRequest_CreateOrder(
            [Frozen] Mock<IOrderRepository> orderRepositoryMock,
            [Frozen] Mock<IShoppingCartRepository> shoppingCartRepositoryMock,
            [Frozen] Mock<IDateTimeProvider> dateTimeProviderMock,
            CartEntity cart,
            DateTime now,
            CreateOrderRequest request,
            CreateOrderHandler sut
            )
        {
            shoppingCartRepositoryMock
                .Setup(x => x.GetCartById(request.CustomerId))
                .ReturnsAsync(cart);

            dateTimeProviderMock
                .Setup(x => x.UtcNow())
                .Returns(now);

            var result = await sut.Handle(request, default);

            result.IsSuccessful.Should().BeTrue();

            orderRepositoryMock.Verify(x => x.Create(It.Is<OrderEntity>(order => 
                order.CustomerId == request.CustomerId &&
                order.TotalAmount == cart.Items.Sum(i => i.TotalPrice()) &&
                order.OrderDate == now)), Times.Once());

            shoppingCartRepositoryMock.Verify(x => x.Clear(request.CustomerId), Times.Once());
        }

        [Theory]
        [AutoMoqInlineData]
        public async Task Handle_ShoppingCartNotFound_ReturnsFailure(
            [Frozen] Mock<IShoppingCartRepository> shoppingCartRepositoryMock,
            [Frozen] Mock<IOrderRepository> orderRepositoryMock,
            CreateOrderRequest request,
            CreateOrderHandler sut
            )
        {
            shoppingCartRepositoryMock
                .Setup(x => x.GetCartById(request.CustomerId))
                .ReturnsAsync((CartEntity?)null);

            var result = await sut.Handle(request, default);

            result.IsSuccessful.Should().BeFalse();

            orderRepositoryMock.Verify(x => x.Create(It.IsAny<OrderEntity>()), Times.Never());
        }
    }
}
