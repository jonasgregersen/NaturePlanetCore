using DataAccessLayer.Context;
using DataAccessLayer.Mappers;
using DataAccessLayer.Repositories;

namespace Business.Model;
using DataTransferLayer.Model;

public class ProductBLL: Component
{
    private readonly ProductRepository _productRepository;

    public ProductBLL(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public override double getPrice()
    {
        return 0;
    }

    public async Task<List<ProductDto>> getAllProducts()
    {
        var products = await _productRepository.GetAllProducts();
        if (!products.Any())
        {
            throw new Exception("Ingen produkter fundet.");
        }

        return products.Select(p => ProductMapper.Map(p)).ToList();
    }

    public async Task<ProductDto> GetProductByName(string productName)
    {
        var products = await _productRepository.GetAllProducts();
        return  ProductMapper.Map(products.FirstOrDefault(p => p.Name == productName));

    }

    public async Task<List<ProductDto>> getAllProductsByCategory(string? category, string? category2 = null, string? category3 = null)
    {
        var products = await _productRepository.GetAllProducts();
        if (!products.Any())
        {
            throw new Exception("Ingen produkter fundet.");
        }
        return products
            .Where(p => 
                category == null || p.Product_Category_1 == category &&
                category2 == null || p.Product_Category_2 == category2 &&
                category3 == null || p.Product_Category_3 == category3
                )
            .Select(p => ProductMapper.Map(p)).ToList();
    }

    public async Task<List<ProductDto>> SearchProducts(string query)
    {
        var products = await _productRepository.GetAllProducts();
        var formattedQuery = query.ToLower().Trim();
        var candidates = products.Where(p => p.Name.ToLower().Contains(formattedQuery)).ToList();
        if (!candidates.Any())
        {
            throw new Exception("Ingen produkter fundet.");
        }
        return candidates.Select(p => ProductMapper.Map(p)).ToList();
    }
}