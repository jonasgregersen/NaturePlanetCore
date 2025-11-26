using System.Diagnostics;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NaturePlanetSolutionCore.Models;

namespace NaturePlanetSolutionCore.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
   

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;

    }

    public IActionResult SetLanguage(string culture)
    {
        var requestCulture = new RequestCulture(culture, culture);

        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(requestCulture),
            new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1)
            }
        );

        return Redirect(Request.Headers["Referer"].ToString());
    }


    public IActionResult Index()
    {
       
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}