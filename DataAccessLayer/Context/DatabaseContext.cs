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
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=tcp:10.10.131.172,1433;Database=NaturePlanet;User Id=sa;Password=12345;TrustServerCertificate=True;");
    }

    


}