using Business.Model;
using DataAccessLayer.Repositories;
using DataTransferLayer.Model;
using Microsoft.Extensions.Caching.Memory;
using Product = DataAccessLayer.Model.DALProduct;
using Business.Constants;

namespace Business.Services;

public class CacheService
{
    private readonly IMemoryCache _cache;
    private readonly ProductRepository _productRepository;
    
    public CacheService(IMemoryCache cache, ProductRepository productRepository)
    {
        _cache = cache;
        _productRepository = productRepository;
    }

    private List<ProductCategory> BuildHierarchy(List<Product> products)
    {
        var level1Groups = products
            .Where(p => p.Product_Category_1 != null && !string.IsNullOrWhiteSpace(p.Product_Category_1))
            .GroupBy(p => p.Product_Category_1)
            .Select(p => new ProductCategory() {Name = p.Key})
            .ToList();

        foreach (var lvl1 in level1Groups)
        {
            var level2Groups = products
                .Where(p => p.Product_Category_2 != null && p.Product_Category_1 == lvl1.Name)
                .GroupBy(p => p.Product_Category_2)
                .Select(p => new ProductCategory() { Name = p.Key })
                .ToList();

            lvl1.Children = level2Groups;

            foreach (var lvl2 in level2Groups)
            {
                var level3Groups = products
                    .Where(p => p.Product_Category_3 != null && p.Product_Category_2 == lvl2.Name && p.Product_Category_1 == lvl1.Name)
                    .GroupBy(p => p.Product_Category_3)
                    .Select(p => new ProductCategory() { Name = p.Key})
                    .ToList();
                
                lvl2.Children = level3Groups;
            }
        }

        return level1Groups;
    }

    public async Task<List<ProductCategory>?> GetCategoryTreeAsync()
    {
        return await _cache.GetOrCreateAsync(CacheKeys.CategoryTreeKey, async (entry) =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);

            var products = await _productRepository.GetAllProducts();

            return BuildHierarchy(products); 
        });
    }
    
    public async Task<List<Product>?> GetProductsAsync()
    {
        return await _cache.GetOrCreateAsync(CacheKeys.ProductsKey, async (entry) =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);

            var products = await _productRepository.GetAllProducts();

            return products;
        });
    }
    
    public void InvalidateCategoryTree()
    {
        _cache.Remove(CacheKeys.CategoryTreeKey);
    }
    
    public void InvalidateProducts()
    {
        _cache.Remove(CacheKeys.ProductsKey);
    }

}