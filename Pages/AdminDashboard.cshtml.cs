using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLayer.Model; // Adjust if ApplicationUser is in a different namespace
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
public class AdminDashboardModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminDashboardModel(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public IList<ApplicationUser> Users { get; set; }

    public async Task OnGetAsync()
    {
        Users = await Task.FromResult(_userManager.Users.ToList());
    }
}