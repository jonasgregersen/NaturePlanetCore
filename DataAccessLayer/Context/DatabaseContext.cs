using DataAccessLayer.Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Context;

public class DatabaseContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=tcp:10.10.132.118,1433;Database=NaturePlanet;User Id=sa;Password=12345;TrustServerCertificate=True;");
    }
}