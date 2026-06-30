using ECommerceApp.Application.UseCases.Queries.GetCartItem;
using ECommerceApp.Infrastructure.Data;
using ECommerceApp.Infrastructure.Models;
using ECommerceApp.IntegrationTests.AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http.Json;

namespace ECommerceApp.IntegrationTests.UseCases
{
    public class GetCartItemTests
    {
        [Theory]
        [IntegrationHostInlineData]
        public async Task GetCart_CartFound_ReturnsDetails(
            IntegrationTestHostBuilder integrationTestHostBuilder)
        {
            const int cartId = 1;
            const int customerId = 207;
            const decimal total = 251m;

            var expectedItem = new CartItemDto
            {
                ProductId = 120,
                ProductName = "ProductNameTest",
                Price = 65m,
                Quantity = 69
            };

            var databaseName = Guid.NewGuid().ToString();

            using var host = integrationTestHostBuilder(services =>
            {
                services.RemoveAll<DbContextOptions<AppDbContext>>();
                services.RemoveAll<DbContextOptions>();
                services.RemoveAll<IDbContextOptionsConfiguration<AppDbContext>>();

                services.AddDbContext<AppDbContext>(options =>
                    options.UseInMemoryDatabase(databaseName));
            });

            using (var scope = host.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                dbContext.Products.Add(new Product
                {
                    Id = expectedItem.ProductId,
                    Name = expectedItem.ProductName,
                    Price = expectedItem.Price,
                    Quantity = 100
                });

                dbContext.Carts.Add(new Cart
                {
                    Id = cartId,
                    CustomerId = customerId,
                    Total = total,
                    Items = new List<CartItem>
                    {
                        new CartItem
                        {
                            ProductId = expectedItem.ProductId,
                            Price = expectedItem.Price,
                            Quantity = expectedItem.Quantity
                        }
                    }
                });

                await dbContext.SaveChangesAsync();
            }

            using var client = host.CreateClient();

            var cart = await client.GetFromJsonAsync<CartDto>($"/cart?customerId={customerId}");

            cart.Should().BeEquivalentTo(new CartDto
            {
                CartId = cartId,
                CustomerId = customerId,
                Total = total,
                Items = new List<CartItemDto>
                {
                    expectedItem
                }
            });
        }
    }
}