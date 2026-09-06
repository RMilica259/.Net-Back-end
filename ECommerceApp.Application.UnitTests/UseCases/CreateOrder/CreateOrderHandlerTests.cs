using AutoFixture.Xunit2;
using ECommerceApp.Application.IRepository;
using ECommerceApp.Application.UseCases.Commands.CreateOrder;
using ECommerceApp.Domain.Date;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.ValueObjects;
using FluentAssertions;
using Moq;

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
            DateTime now,
            CreateOrderRequest request,
            CreateOrderHandler sut)
        {
            var cart = new CartEntity(request.CustomerId);

            var cartItem = new CartItemEntity(
                17,
                25m,
                Quantity.FromInt(2));

            cart.AddItem(cartItem);

            shoppingCartRepositoryMock
                .Setup(x => x.GetById(request.CustomerId))
                .ReturnsAsync(cart);

            dateTimeProviderMock
                .Setup(x => x.UtcNow())
                .Returns(now);

            var result = await sut.Handle(request, default);

            result.IsSuccessful.Should().BeTrue();

            orderRepositoryMock.Verify(
                x => x.Create(It.Is<OrderEntity>(order =>
                    order.CustomerId == request.CustomerId &&
                    order.TotalAmount == cart.Total() &&
                    order.OrderDate == now &&
                    order.ShippingCity == request.ShippingAddress.City &&
                    order.ShippingStreet == request.ShippingAddress.Street &&
                    order.ShippingHouseNumber ==
                        request.ShippingAddress.HouseNumber &&
                    order.ShippingZipCode ==
                        request.ShippingAddress.ZipCode &&
                    order.Items.Count == 1 &&
                    order.Items.Single().ProductId == cartItem.ProductId &&
                    order.Items.Single().UnitPrice == cartItem.Price &&
                    order.Items.Single().Quantity.Value ==
                        cartItem.Quantity.Value)),
                Times.Once());

            shoppingCartRepositoryMock.Verify(
                x => x.Delete(request.CustomerId),
                Times.Once());
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
                .Setup(x => x.GetById(request.CustomerId))
                .ReturnsAsync((CartEntity?)null);

            var result = await sut.Handle(request, default);

            result.IsSuccessful.Should().BeFalse();

            orderRepositoryMock.Verify(x => x.Create(It.IsAny<OrderEntity>()), Times.Never());
        }

        [Theory]
        [AutoMoqInlineData]
        public async Task Handle_EmptyShoppingCart_ReturnsFailure(
            [Frozen] Mock<IShoppingCartRepository> shoppingCartRepositoryMock,
            [Frozen] Mock<IOrderRepository> orderRepositoryMock,
            CreateOrderRequest request,
            CreateOrderHandler sut)
        {
            var cart = new CartEntity(request.CustomerId);

            shoppingCartRepositoryMock
                .Setup(x => x.GetById(request.CustomerId))
                .ReturnsAsync(cart);

            var result = await sut.Handle(request, default);

            result.IsSuccessful.Should().BeFalse();
            result.Error!.ErrorCode.Should().Be("CARTT003");

            orderRepositoryMock.Verify(
                x => x.Create(It.IsAny<OrderEntity>()),
                Times.Never());

            shoppingCartRepositoryMock.Verify(
                x => x.Delete(It.IsAny<int>()),
                Times.Never());
        }
    }
}
