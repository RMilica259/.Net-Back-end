using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Application.IServices
{
    public interface IStockAvailability
    {
        Task<int> GetAvailableQuantity(int productId);
    }
}
