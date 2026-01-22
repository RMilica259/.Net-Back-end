using ECommerceApp.Application.UseCases.Queries.GetCartItem;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Infrastructure.Data;
using ECommerceApp.Infrastructure.Models;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ECommerceApp.IntegrationTests.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;


namespace ECommerceApp.IntegrationTests.UseCases
{
    public class GetCartItemTests
    {
        [Theory]
        [IntegrationHostInlineData(1)]
        public async Task GetCart_CartFound_ReturnsDetails(
            int cartId,
            int customerId,
            decimal total,
            List<CartItemDto> cartItems,
            IntegrationTestHostBuilder integrationTestHostBuilder
            )
        {
            using var host = integrationTestHostBuilder();
            using var client = host.CreateClient();

            var cart = await client.GetFromJsonAsync<CartDto>($"/cart?customerId={customerId}");

            cart.Should()
                .BeEquivalentTo(new CartDto()
                {
                    CartId = cartId,
                    CustomerId = customerId,
                    Total = total,
                    Items = cartItems
                });
        }
    }
}
