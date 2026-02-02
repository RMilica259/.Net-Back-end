using ECommerceApp.Application.IServices;
using ECommerceApp.Application.Services;
using ECommerceApp.Application.UseCases.Queries.GetCartItem;
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

            services.AddScoped<IStockAvailability, StockAvailabilityMock>();

            services.AddScoped<IStock, ExternalStock>();
            services.Decorate<IStock, LocalFirstStockDecorator>();

            services.AddScoped<Discount>();
        }
    }
}
