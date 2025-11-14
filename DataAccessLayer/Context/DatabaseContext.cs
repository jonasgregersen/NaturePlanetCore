using DataAccessLayer.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Context;

public class DatabaseContext : IdentityDbContext<ApplicationUser>
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {

    }

    public DbSet<Product> Products { get; set; }

    public DbSet<Order> Orders { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=tcp:natureplanetprojekt.database.windows.net,1433;Initial Catalog=NaturePlanet;Persist Security Info=False;User ID=natureplanetadmin;Password=NaturePlanet123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    }

    


}