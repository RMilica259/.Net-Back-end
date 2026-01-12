using ECommerceApp.Application.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
