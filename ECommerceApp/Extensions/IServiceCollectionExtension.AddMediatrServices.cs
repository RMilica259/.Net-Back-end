using ECommerceApp.Application.UseCases.Commands.CreateOrder;

namespace ECommerceApp.Web.Extensions
{
    public static partial class IServiceCollectionExtension
    {
        public static void AddMediatRServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateOrderRequest).Assembly));
        }
    }
}
