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

    public List<Product> getAllProducts()
    {
        var products = _productRepository.GetAllProducts();
        if (!products.Any())
        {
            throw new Exception("Ingen produkter fundet.");
        }

        return products.Select(p => ProductMapper.Map(p)).ToList();
    }
}