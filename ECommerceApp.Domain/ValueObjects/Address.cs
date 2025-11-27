using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Domain.ValueObjects
{
    public record Address(string City, string Street, string HouseNumber, string ZipCode);
}
