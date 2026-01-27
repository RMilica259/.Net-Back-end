namespace ECommerceApp.Application.UseCases.Queries.GetCartItem
{
    public interface IGetCartQuery
    {
        Task<CartDto?> Execute(int customerId);
    }
}
