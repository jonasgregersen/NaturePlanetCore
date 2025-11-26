using Business.Services;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace NaturePlanetSolutionCore.ViewComponents;

public class CategoryMenuViewComponent : ViewComponent
{
    private readonly CategoryService _service;
    
    public CategoryMenuViewComponent(CategoryService service)
    {
        _service = service;
    }
    
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var categories = await _service.GetCategoryTreeAsync();

        return View(categories);
    }
}