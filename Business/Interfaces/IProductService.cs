// Business/Interfaces/IProductService.cs
using DataTransferLayer.Model;

namespace Business.Interfaces
{
    public interface IProductService
    {
        Task CreateProduct(ProductDto product);
        Task<List<ProductDto>> SearchProductsAsync(string query);
        Task<List<ProductDto>> GetRecommendedProductsAsync(string productId, int limit = 5);
        Task<List<ProductDto>> GetUserRecommendationsAsync(string userId, int limit = 5);
    }
}