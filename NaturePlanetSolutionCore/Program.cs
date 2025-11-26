using Business.Model;
using Business.Services;
using DataAccessLayer.Context;
using DataAccessLayer.Model;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer("Server=localhost;Database=NaturePlanetDB;User Id=sa;Password=reallyStrongPwd123;MultipleActiveResultSets=true;TrustServerCertificate=true"));



builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    //options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.User.RequireUniqueEmail = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;

})
    .AddEntityFrameworkStores<DatabaseContext>()
    .AddDefaultTokenProviders();

//builder.Services.AddLocalization(options =>
//{
//    options.ResourcesPath = "Resources";
//});

//var supportedLanguages = new string[] { "en", "da", "fr", "de" };

//builder.Services.Configure<RequestLocalizationOptions>(options =>
//{
//    options.SetDefaultCulture("en");
//    options.AddSupportedCultures(supportedLanguages);
//    options.AddSupportedUICultures(supportedLanguages);
//});

builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<ProductBLL>();
builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<OrderBLL>();
builder.Services.AddScoped<CategoryService>();


builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".Orders.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(120);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddMemoryCache();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/User/Login";
    options.AccessDeniedPath = "/User/Login";
});

var app = builder.Build();

// Seed roles
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = { "Admin", "User" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();

//var locOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
//app.UseRequestLocalization(locOptions.Value);

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
    name: "productSearch",
    pattern: "Products/Search",
    defaults: new { controller = "Products", action = "Search" });

app.MapControllerRoute(
    name: "productDetails",
    pattern: "Products/Details/{productName}",
    defaults: new { controller = "Products", action = "Details" });

app.MapControllerRoute(
    name: "productCategory",
    pattern: "Products/{category1?}/{category2?}/{category3?}",
    defaults: new { controller = "Products", action = "FilterByCategory" });





app.Run();