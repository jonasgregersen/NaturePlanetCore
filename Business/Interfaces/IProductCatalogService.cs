// Business/Interfaces/IProductCatalogService.cs
using DataTransferLayer.Model;

namespace Business.Interfaces
{
    public interface IProductCatalogService
    {
        Task<ProductDto> GetProductById(string productId);
        Task<List<ProductDto>> GetAllProductsByCategoryAsync(string? category, string? category2 = null, string? category3 = null);
    }
}