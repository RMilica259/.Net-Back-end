using ECommerceApp.Application.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Application.Services
{
    public class StockAvailability
    {
        private readonly IStockAvailability _stockAvailability;

        public StockAvailability(IStockAvailability stockAvailability)
        {
            _stockAvailability = stockAvailability;
        }

        public async Task<bool> IsQuantityAvailable(int localStock, int productId, int requestedQuantity)
        {
            if (localStock >= requestedQuantity)
                return true;

            var stock = await _stockAvailability.GetAvailableQuantity(productId);

            if (stock + localStock >= requestedQuantity)
                return true;

            return false;
        }
    }
}
