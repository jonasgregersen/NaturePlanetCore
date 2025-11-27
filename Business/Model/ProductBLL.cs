using Business.Interfaces;
using Business.Services;
using DataAccessLayer.Context;
using DataAccessLayer.Mappers;
using DataAccessLayer.Repositories;

namespace Business.Model;
using DataTransferLayer.Model;

public class ProductBLL: Component, IProductService
{
    private readonly ProductRepository _productRepository;
    private readonly CacheService _cache;
    private readonly IRecommendationService _recommendationService;

    public ProductBLL(ProductRepository productRepository, CacheService cache, IRecommendationService recommendationService)
    {
        _productRepository = productRepository;
        _cache = cache;
        _recommendationService = recommendationService;
    }
    public override double getPrice()
    {
        return 0;
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
        return  ProductMapper.Map(products.FirstOrDefault(p => p.Name == productName));

    }

    public async Task<ProductDto> GetProductById(string productId)
    {
        var products = await _cache.GetProductsAsync();
        var product = products.Find(p => p.ProductId == productId);
        if (product != null)
        {
            return ProductMapper.Map(product);
        }
        return null;
    }


    public async Task<List<ProductDto>> GetAllProductsByCategoryAsync(string? category, string? category2 = null,
        string? category3 = null)
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
                (category3 == null || p.Product_Category_3 == category3)
                )
            .Select(p => ProductMapper.Map(p)).ToList();
    }

    public async Task<List<ProductDto>> GetRecommendedProductsAsync(string productId, int limit = 5)
    {
        return await _recommendationService.GetSimilarProductsByCategoryAsync(productId, limit);
    }
    
    public async Task<List<ProductDto>> GetUserRecommendationsAsync(string userId, int limit = 5)
    {
        return await _recommendationService.GetRecommendedProductsForUserAsync(userId, limit);
    }

    public async Task<List<ProductDto>> GetPopularProductsAsync(int limit = 5)
    {
        return await _recommendationService.GetPopularProductsAsync(limit);
    }

    public async Task<List<ProductDto>> SearchProductsAsync(string query)
    {
        var products = await _cache.GetProductsAsync();
        var formattedQuery = query.ToLower().Trim();
        var candidates = products.Where(p => p.Name.ToLower().Contains(formattedQuery)).ToList();
        if (!candidates.Any())
        {
            throw new Exception("Ingen produkter fundet.");
        }
        return candidates.Select(p => ProductMapper.Map(p)).ToList();
    }

    public async Task CreateProduct(ProductDto product)
    {
        
        if (product.Name == null)
        {
            throw new Exception("Produkt navn er ikke angivet.");
        }
        
        if (product.EAN == null)
        {
            throw new Exception("EAN er ikke angivet.");
        }
        
        if (product.ErpSource == null)
        {
            throw new Exception("ERP source er ikke angivet.");
        }
        
        if (product.Product_Category_1 == null)
        {
            throw new Exception("1. kategorier er ikke angivet.");
        }
        
        if (product.Product_Category_2 == null)
        {
            throw new Exception("2. kategorier er ikke angivet.");
        }
        
        if (product.Product_Category_3 == null)
        {
            throw new Exception("3. kategorier er ikke angivet.");
        }
        
        if (product.Weight == null)
        {
            throw new Exception("Vægt er ikke angivet.");
        }
        
        if (product.Segment == null)
        {
            throw new Exception("Segment er ikke angivet.");
        }

        if (product.Product_Category_1 == null)
        {
            throw new Exception("1. kategorier er ikke angivet.");
        }

        if (product.Product_Category_2 == null)
        {
            throw new Exception("2. kategorier er ikke angivet.");
        }

        if (product.Product_Category_3 == null)
        {
            throw new Exception("3. kategorier er ikke angivet.");
        }
        
        var products = await _cache.GetProductsAsync();
        if (products.Any(p => p.ProductId.ToLower() == product.Id.ToLower()))
        {
            throw new Exception("Produktet med id findes allerede.");
        }

        if (products.Any(p => p.Name.ToLower() == product.Name.ToLower()))
        {
            throw new Exception("Der findes et produkt med samme navn.");
        }

        if (products.Any(p => p.EAN.ToLower() == product.EAN.ToLower()))
        {
            throw new Exception("Der findes et produkt med denne EAN");
        }
        _cache.InvalidateProducts();
        _cache.InvalidateCategoryTree();
        _productRepository.CreateProduct(ProductMapper.Map(product));
    }
}