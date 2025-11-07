using DataAccessLayer.Context;
using Microsoft.AspNetCore.Mvc;

namespace NaturePlanetSolutionCore.ViewComponents;

public class CategoryMenuViewComponent : ViewComponent
{
    private readonly DatabaseContext _context;
    public CategoryMenuViewComponent(DatabaseContext context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke()
    {
        var categories = _context.Products
            .Where(p => p.Product_Category_1 != null)
            .Select(p => p.Product_Category_1)
            .Distinct()
            .OrderBy(c => c)
            .ToList();
        return View(categories);
    }
}