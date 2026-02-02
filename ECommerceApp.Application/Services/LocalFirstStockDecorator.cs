using ECommerceApp.Application.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Application.Services
{
    public class LocalFirstStockDecorator : IStock
    {
        private readonly IStock _innerStock;
        public LocalFirstStockDecorator(IStock innerStock)
        {
            _innerStock = innerStock;
        }

        public Task<bool> IsQuantityAvailable(int localStock, int productId, int requestedQuantity)
        {
            if (localStock >= requestedQuantity)
                return Task.FromResult(true);

            return _innerStock.IsQuantityAvailable(localStock, productId, requestedQuantity);
        }
    }
}
