using ECommerceApp.Application.IRepository;
using ECommerceApp.Infrastructure.Repository;

namespace ECommerceApp.Web.Extensions
{
    public static partial class IServiceCollectionExtension
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
        }
    }
}
