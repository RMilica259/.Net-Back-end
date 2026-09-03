using MediatR;

namespace ECommerceApp.Application.UseCases.Queries.GetCustomerById
{
    public class GetCustomerByIdRequest : IRequest<CustomerDto?>
    {
        public int CustomerId { get; set; }
    }
}
