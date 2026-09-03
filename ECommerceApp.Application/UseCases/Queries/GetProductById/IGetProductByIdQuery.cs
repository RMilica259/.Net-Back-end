namespace ECommerceApp.Application.UseCases.Queries.GetProductById
{
    public interface IGetProductByIdQuery
    {
        Task<ProductDto?> Execute(int productId);
    }
}
