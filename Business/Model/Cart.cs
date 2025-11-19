using System.Security.Cryptography;
using DataTransferLayer.Model;

namespace Business.Model;

public class Cart
{
    public List<ProductDto> Products { get; set; } = new List<ProductDto>();
    public int OrderNumber { get; set; }

    public void AddProduct(ProductDto product)
    {
        Products.Add(product);
    }

    public void RemoveProduct(ProductDto product)
    {
        Products.Remove(product);
    }

    public int GenerateOrderNumber()
    {
        return RandomNumberGenerator.GetInt32(10000);
    }
}