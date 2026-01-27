using ECommerceApp.Application.IServices;

namespace ECommerceApp.Infrastructure
{
    public class StockAvailabilityMock : IStockAvailability
    {
        public Task<int> GetAvailableQuantity(int productId)
        {
            return Task.FromResult(0);
        }
    }
}
