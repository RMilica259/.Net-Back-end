using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Application.UseCases.Queries.GetCartItem
{
    public interface IGetCartQuery
    {
        Task<CartDto?> Execute(int customerId);
    }
}
