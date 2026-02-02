using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Application.IServices
{
    public interface IStock
    {
        public Task<bool> IsQuantityAvailable(int localStock, int productId, int requestedQuantity);
    }
}
