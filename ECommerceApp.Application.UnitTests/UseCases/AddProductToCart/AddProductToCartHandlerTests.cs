using AutoFixture.Xunit2;
using ECommerceApp.Application.IRepository;
using ECommerceApp.Application.IServices;
using ECommerceApp.Application.Services;
using ECommerceApp.Application.UseCases.Commands.AddProductToCart;
using ECommerceApp.Domain.Entities;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Application.UnitTests.UseCases.AddProductToCart
{
    public class AddProductToCartHandlerTests
    {
        [Theory]
        [AutoMoqInlineData]
        public async Task Handle_ValidRequest_AddProductToCart(
            [Frozen] Mock<IProductRepository> productRepositoryMock,
            [Frozen] Mock<IShoppingCartRepository> shoppingCartRepositoryMock,
            [Frozen] Mock<StockAvailability> stockAvailabilityMock,
            ProductEntity product,
            AddProductToCartRequest request,
            AddProductToCartHandler sut
            )
        {
            productRepositoryMock
                .Setup(x => x.GetById(request.ProductId))
                .ReturnsAsync(product);

            stockAvailabilityMock
                .Setup(x => x.IsQuantityAvailable(
                    product.Quantity.Value,
                    request.ProductId,
                    request.Quantity))
                .ReturnsAsync(true);

            var result = await sut.Handle(request, default);

            result.IsSuccessful.Should().BeTrue();

            shoppingCartRepositoryMock.Verify(
                x => x.Add(It.Is<CartItemEntity>(item =>
                    item.ProductId == request.ProductId &&
                    item.Quantity.Value == request.Quantity)), 
                Times.Once());
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

            shoppingCartRepositoryMock.Verify(x => x.Add(It.IsAny<CartItemEntity>()), Times.Never());
        }

        [Theory]
        [AutoMoqInlineData]
        public async Task Handle_NotEnoughStock_ReturnsFailure(
            [Frozen] Mock<IProductRepository> productRepositoryMock,
            [Frozen] Mock<IShoppingCartRepository> shoppingCartRepositoryMock,
            [Frozen] Mock<StockAvailability> stockAvailabilityMock,
            ProductEntity product,
            AddProductToCartRequest request,
            AddProductToCartHandler sut
            )
        {
            productRepositoryMock
                .Setup(x => x.GetById(request.ProductId))
                .ReturnsAsync(product);

            stockAvailabilityMock
                .Setup(x => x.IsQuantityAvailable(
                    product.Quantity.Value,
                    request.ProductId,
                    request.Quantity))
                .ReturnsAsync(false);

            var result = await sut.Handle(request, default);

            result.IsSuccessful.Should().BeFalse();

            shoppingCartRepositoryMock.Verify(x => x.Add(It.IsAny<CartItemEntity>()), Times.Never());
        }
    }
}
