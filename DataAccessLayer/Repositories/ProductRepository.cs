using DataAccessLayer.Context;
using DataAccessLayer.Model;

namespace DataAccessLayer.Repositories;

public class ProductRepository
{
    private readonly DatabaseContext _context;

    public ProductRepository(DatabaseContext context)
    {
        _context = context;
    }

    public void CreateProduct(string id,string productName, string ean, string erpSource, bool active, string productCategory1,
        string productCategory2, string productCategory3, int quantityInBag, decimal productWeight,
        string productSegment)
    {
        var product = new DALProduct
        {
            ProductId = id,
            Name = productName,
            EAN = ean,
            ERP_Source = erpSource,
            Active = active,
            Purchase_quantity_step = quantityInBag,
            Weight = productWeight,
            Segment = productSegment,
            Product_Category_1 = productCategory1,
            Product_Category_2 = productCategory2,
            Product_Category_3 = productCategory3
        };
        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public DALProduct GetProduct(string id)
    {
        return _context.Products.FirstOrDefault(e => e.ProductId == id);
    }

    public List<DALProduct> GetAllProducts()
    {
        return _context.Products.ToList();
    }

    public List<string> GetSubCategory_2_Of(string categoryName)
    {
        return _context.Products
            .Where(p => p.Product_Category_1 == categoryName)
            .Select(p => p.Product_Category_2).ToList()
            .Distinct()
            .OrderBy(p => p)
            .ToList();
    }

    public List<string> GetSubCategory_3_Of(string categoryName)
    {
        return _context.Products
            .Where(p => p.Product_Category_2 == categoryName)
            .Select(p => p.Product_Category_3)
            .Distinct()
            .OrderBy(p => p)
            .ToList();
    }

    public List<string> GetCategories()
    {
        return _context.Products
            .Where(p => p.Product_Category_1 != null)
            .Select(p => p.Product_Category_1)
            .Distinct()
            .OrderBy(p => p)
            .ToList();
    }
    public void RemoveProduct(DALProduct product)
    {
        _context.Products.Remove(product);
        _context.SaveChanges();
    }
}