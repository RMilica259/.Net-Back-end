using ECommerceApp.Application.IServices;
using ECommerceApp.Application.Services;
using ECommerceApp.Application.UseCases.Queries.GetAllProducts;
using ECommerceApp.Application.UseCases.Queries.GetCartItem;
using ECommerceApp.Application.UseCases.Queries.GetProductById;
using ECommerceApp.Domain.Date;
using ECommerceApp.Infrastructure;
using ECommerceApp.Infrastructure.Queries;

namespace ECommerceApp.Web.Extensions
{
    public static partial class IServiceCollectionExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();

            services.AddScoped<IGetCartQuery, GetCartQuery>();
            services.AddScoped<IGetProductByIdQuery, GetProductByIdQuery>();
            services.AddScoped<IGetAllProductsQuery, GetAllProductsQuery>();

            services.AddScoped<IStockAvailability, StockAvailabilityMock>();

            services.AddScoped<IStock, ExternalStock>();
            services.Decorate<IStock, LocalFirstStockDecorator>();

            services.AddScoped<Discount>();
        }
    }
}
