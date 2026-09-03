using MediatR;

namespace ECommerceApp.Application.UseCases.Queries.GetCustomerById
{
    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdRequest, CustomerDto?>
    {
        private readonly IGetCustomerByIdQuery _query;

        public GetCustomerByIdHandler(IGetCustomerByIdQuery query)
        {
            _query = query;
        }

        public async Task<CustomerDto?> Handle(GetCustomerByIdRequest request, CancellationToken cancellationToken)
        {
            return await _query.Execute(request.CustomerId);
        }
    }
}
