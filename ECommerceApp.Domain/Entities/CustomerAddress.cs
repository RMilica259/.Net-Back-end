using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Domain.Entities
{
    public class CustomerAddressEntity
    {
        public CustomerAddressEntity(AddressEntity address, bool isDefault = false) {
            Address = address;
            IsDefault = isDefault;
        }
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public AddressEntity Address { get; }
        public bool IsDefault { get; private set; }
        public void SetAsDefault()
        {
            IsDefault = true;
        }
    }
}
