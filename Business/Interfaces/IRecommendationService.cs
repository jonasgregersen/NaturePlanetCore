using DataTransferLayer.Model;

namespace Business.Interfaces;


public interface IRecommendationService
{
    Task<List<ProductDto>> GetSimilarProductsByCategoryAsync(string productId, int limit = 5);
    Task<List<ProductDto>> GetRecommendedProductsForUserAsync(string userId, int limit = 5);
    Task<List<ProductDto>> GetPopularProductsAsync(int limit = 5);
}