using AutoFixture.Xunit2;
using ECommerceApp.Application.IRepository;
using ECommerceApp.Application.Services;
using ECommerceApp.Application.UseCases.Commands.AddProductToCart;
using ECommerceApp.Domain.Entities;
using FluentAssertions;
using Moq;

namespace ECommerceApp.Application.UnitTests.UseCases.AddProductToCart
{
    public class AddProductToCartHandlerTests
    {
        [Theory]
        [AutoMoqInlineData]
        public async Task Handle_ValidRequest_AddProductToCart(
            [Frozen] Mock<IProductRepository> productRepositoryMock,
            [Frozen] Mock<IShoppingCartRepository> shoppingCartRepositoryMock,
            ProductEntity product,
            AddProductToCartRequest request,
            AddProductToCartHandler sut
            )
        {
            productRepositoryMock
                .Setup(x => x.GetById(request.ProductId))
                .ReturnsAsync(product);

            var result = await sut.Handle(request, default);

            result.IsSuccessful.Should().BeTrue();

            shoppingCartRepositoryMock.Verify(
                x => x.Update(It.Is<CartEntity>(c =>
                    c.Items.Any(i =>
                        i.ProductId == request.ProductId &&
                        i.Quantity.Value == request.Quantity))),
                Times.Once);
        }

        [Theory]
        [AutoMoqInlineData]
        public async Task Handle_ProductNotFound_ReturnsFailure(
            [Frozen] Mock<IProductRepository> productRepositoryMock,
            [Frozen] Mock<IShoppingCartRepository> shoppingCartRepositoryMock,
            AddProductToCartRequest request,
            AddProductToCartHandler sut
            )
        {
            productRepositoryMock
                .Setup(x => x.GetById(request.ProductId))
                .ReturnsAsync((ProductEntity?)null);

            var result = await sut.Handle(request, default);

            result.IsSuccessful.Should().BeFalse();

            shoppingCartRepositoryMock.Verify(x => x.Update(It.IsAny<CartEntity>()), Times.Never);
        }

        [Theory]
        [AutoMoqInlineData]
        public async Task Handle_NotEnoughStock_ReturnsFailure(
            [Frozen] Mock<IProductRepository> productRepositoryMock,
            [Frozen] Mock<IShoppingCartRepository> shoppingCartRepositoryMock,
            ProductEntity product,
            AddProductToCartRequest request,
            AddProductToCartHandler sut
            )
        {
            productRepositoryMock
                .Setup(x => x.GetById(request.ProductId))
                .ReturnsAsync(product);

            var result = await sut.Handle(request, default);

            result.IsSuccessful.Should().BeFalse();

            shoppingCartRepositoryMock.Verify(x => x.Update(It.IsAny<CartEntity>()), Times.Never);
        }
    }
}
