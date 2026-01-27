namespace ECommerceApp.Application.IServices
{
    public interface IStockAvailability
    {
        Task<int> GetAvailableQuantity(int productId);
    }
}
