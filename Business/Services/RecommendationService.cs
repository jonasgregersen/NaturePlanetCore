using Business.Interfaces;
using Business.Model;
using DataTransferLayer.Model;

namespace Business.Services;

public class RecommendationService : IRecommendationService
{
    private readonly IProductCatalogService _productCatalogService;
    private readonly OrderBLL _orderBll;
    
    public RecommendationService(IProductCatalogService productCatalogService, OrderBLL orderBll)
    {
        _productCatalogService = productCatalogService;
        _orderBll = orderBll;
    }

    public async Task<List<ProductDto>> GetSimilarProductsByCategoryAsync(string productId, int limit = 5)
    {
        Console.WriteLine($"Getting similar products for product ID: {productId}");
        var product = await _productCatalogService.GetProductById(productId);
        if (product == null)
        {
            Console.WriteLine($"Product with ID {productId} not found");
            return Enumerable.Empty<ProductDto>().ToList();
        }
        Console.WriteLine($"Found product: {product.Name} in categories: {product.Product_Category_1}, {product.Product_Category_2}, {product.Product_Category_3}");

        var similarProducts = await _productCatalogService.GetAllProductsByCategoryAsync(
            product.Product_Category_1, 
            product.Product_Category_2, 
            product.Product_Category_3);
            
        Console.WriteLine($"Found {similarProducts.Count} similar products");
        var result = similarProducts
            .Where(p => p.Id != productId) // Undgå at anbefale det samme produkt
            .Take(limit)
            .ToList();
            
        return result;
    }

    public async Task<List<ProductDto>> GetRecommendedProductsForUserAsync(string userId, int limit = 5)
    {
        Console.WriteLine($"Getting recommendations for user: {userId}");
        var userOrders = await _orderBll.GetUserOrders(userId);
        Console.WriteLine($"Found {userOrders.Count} orders for user");

        if (!userOrders.Any())
        {
            Console.WriteLine("No orders found, returning popular products");
            return await GetPopularProductsAsync(limit);
        }
        
        var purchasedProducts = userOrders
            .SelectMany(o => o.Products)
            .ToList();
        
        Console.WriteLine($"Found {purchasedProducts.Count} purchased products");
        
        if (!purchasedProducts.Any())
        {
            Console.WriteLine("No products in orders, returning popular products");
            return await GetPopularProductsAsync(limit);
        }
        
        var purchasedProductsOrderedByCount = purchasedProducts
            .GroupBy(p => p)
            .OrderByDescending(g => g.Count())
            .Select(p => p.First())
            .ToList();

        var recommendedProducts = new List<ProductDto>();
        foreach (var product in purchasedProductsOrderedByCount)
        {
            var similarProducts = await GetSimilarProductsByCategoryAsync(product.Id);
            recommendedProducts.AddRange(similarProducts);
        }
        recommendedProducts = recommendedProducts
            .GroupBy(p => p.Id)
            .Select(p => p.First())
            .Where(p => !purchasedProducts.Any(pp => pp.Id == p.Id))
            .Take(limit)
            .ToList();
        return recommendedProducts.Take(limit).ToList();
    }

    public async Task<List<ProductDto>> GetPopularProductsAsync(int limit)
    {
        Console.WriteLine("Getting popular products");
        var orders = await _orderBll.GetAllOrders();
        Console.WriteLine($"Found {orders.Count} total orders");
        
        var orderProducts = orders.SelectMany(o => o.Products).ToList();
        Console.WriteLine($"Found {orderProducts.Count} products in all orders");
        
        var popularProducts = orderProducts
            .GroupBy(p => p.Id)
            .Select(g => new { Product = g.First(), Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(limit)
            .Select(x => x.Product)
            .ToList();
            
        Console.WriteLine($"Returning {popularProducts.Count} popular products");
        return popularProducts;
    }
}