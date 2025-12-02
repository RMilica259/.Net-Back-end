using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Domain.Entities
{
    public class AddressEntity
    {
        public AddressEntity(string city, string street, string houseNumber, string zipCode)
        {
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            ZipCode = zipCode;
        }

        public string City { get; }
        public string Street { get; }
        public string HouseNumber { get; }
        public string ZipCode { get; }
    }
}
