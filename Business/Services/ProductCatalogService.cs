// Business/Services/ProductCatalogService.cs
using Business.Interfaces;
using DataAccessLayer.Mappers;
using DataAccessLayer.Repositories;
using DataTransferLayer.Model;

namespace Business.Services
{
    public class ProductCatalogService : IProductCatalogService
    {
        private readonly CacheService _cache;

        public ProductCatalogService(ProductRepository productRepository, CacheService cache)
        {
            _cache = cache;
        }

        public async Task<ProductDto> GetProductById(string productId)
        {
            var products = await _cache.GetProductsAsync();
            var product = products.Find(p => p.ProductId == productId);
            return product != null ? ProductMapper.Map(product) : null;
        }

        public async Task<List<ProductDto>> GetAllProductsByCategoryAsync(string? category, string? category2 = null, string? category3 = null)
        {
            var products = await _cache.GetProductsAsync();
            if (!products.Any())
            {
                throw new Exception("Ingen produkter fundet.");
            }
            return products
                .Where(p => 
                    p.Product_Category_1 == category &&
                    (category2 == null || p.Product_Category_2 == category2) &&
                    (category3 == null || p.Product_Category_3 == category3))
                .Select(p => ProductMapper.Map(p))
                .ToList();
        }

        public async Task<List<ProductDto>> getAllProductsAsync()
        {
            var products = await _cache.GetProductsAsync();
            if (!products.Any())
            {
                throw new Exception("Ingen produkter fundet.");
            }
            return products.Select(p => ProductMapper.Map(p)).ToList();
        }

        public async Task<ProductDto> GetProductByName(string productName)
        {
            var products = await _cache.GetProductsAsync();
            var product = products.FirstOrDefault(p => p.Name == productName);
            return product != null ? ProductMapper.Map(product) : null;
        }
    }
}