using ECommerceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Application.IRepository
{
    public interface ICustomerRepository
    {
        Task<bool> Exists (int id);
        Task<CustomerEntity?> GetById (int id);
        Task<CustomerEntity?> GetWithAddresses(int id);
    }
}
