using ECommerceApp.Application.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Application.Services
{
    public class ExternalStock : IStock
    {
        private readonly IStockAvailability _externalStock;
        public ExternalStock(IStockAvailability externalStock)
        {
            _externalStock = externalStock;
        }

        public async Task<bool> IsQuantityAvailable(int localStock, int productId, int requestedQuantity)
        {
            var external = await _externalStock.GetAvailableQuantity(productId);
            return (localStock + external) >= requestedQuantity;
        }
    }
}
