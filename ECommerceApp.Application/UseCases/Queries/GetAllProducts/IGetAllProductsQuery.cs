using ECommerceApp.Application.UseCases.Queries.GetProductById;

namespace ECommerceApp.Application.UseCases.Queries.GetAllProducts
{
    public interface IGetAllProductsQuery
    {
        Task<List<ProductDto>> Execute();
    }
}
