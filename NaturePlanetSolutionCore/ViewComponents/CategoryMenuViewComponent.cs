using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace NaturePlanetSolutionCore.ViewComponents;

public class CategoryMenuViewComponent : ViewComponent
{
    private readonly ProductRepository _productRepository;
    public CategoryMenuViewComponent(ProductRepository repository)
    {
        _productRepository = repository;
    }

    public class CategoryViewModel
    {
        public string Name { get; set; }
        public List<SubcategoryViewModel> Subcategories { get; set; } = new();
    }

    public class SubcategoryViewModel
    {
        public string Name { get; set; }
        public List<string> SubSubcategories { get; set; } = new();
    }


    public IViewComponentResult Invoke()
    {
        var categories = _productRepository.GetCategories()
            .Select(cat1 => new CategoryViewModel
            {
                Name = cat1,
                Subcategories = _productRepository.GetSubCategory_2_Of(cat1)
                    .Select(cat2 => new SubcategoryViewModel
                    {
                        Name = cat2,
                        SubSubcategories = _productRepository.GetSubCategory_3_Of(cat2)
                    })
                    .ToList()
            })
            .ToList();

        return View(categories);
    }
}