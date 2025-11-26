using DataAccessLayer.Context;
using DataAccessLayer.Model;
using DataTransferLayer.Model;

namespace DataAccessLayer.Repositories;

public class ProductRepository
{
    private readonly DatabaseContext _context;

    public ProductRepository(DatabaseContext context)
    {
        _context = context;
    }

    public void CreateProduct(DALProduct product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public DALProduct GetProduct(string id)
    {
        return _context.Products.FirstOrDefault(e => e.ProductId == id);
    }

    public async Task<List<DALProduct>> GetAllProducts()
    {
        return _context.Products.ToList();
    }
    public void RemoveProduct(DALProduct product)
    {
        _context.Products.Remove(product);
        _context.SaveChanges();
    }
}