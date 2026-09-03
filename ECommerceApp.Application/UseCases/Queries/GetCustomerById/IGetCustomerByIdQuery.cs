namespace ECommerceApp.Application.UseCases.Queries.GetCustomerById
{
    public interface IGetCustomerByIdQuery
    {
        Task<CustomerDto?> Execute(int customerId);
    }
}
