using DataAccessLayer.Context;
using DataAccessLayer.Model;

namespace DataAccessLayer.Repositories;

public class ProductRepository
{
    private DatabaseContext _context =  new DatabaseContext();

    public void CreateProduct(string productName, int ean, string erpSource, bool active, string productCategory1,
        string productCategory2, string productCategory3, int quantityInBag, double productWeight,
        string productSegment)
    {
        var product = new Product(productName, ean, erpSource, active, quantityInBag, productWeight, productSegment, productCategory1, productCategory2, productCategory3);
        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public Product GetProduct(int id)
    {
        return _context.Products.FirstOrDefault(e => e.Id == id);
    }

    public void RemoveProduct(Product product)
    {
        _context.Products.Remove(product);
        _context.SaveChanges();
    }
}