using ECommerceApp.Domain.OperationResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Application.UseCases.Commands.CreateOrder
{
    public class CreateOrderRequest : IRequest<Result>
    {
        public int CustomerId { get; set; }
        public AddressDto ShippingAddress { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class AddressDto
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string ZipCode { get; set; }
    }
}
