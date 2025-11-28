using ECommerceApp.Domain.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ECommerceApp.Application.UseCases.Commands.AddProductToCart
{
    public class AddProductToCartRequest : IRequest<Result>
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
