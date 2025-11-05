using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Context;

public class DatabaseContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("DefaultConnection");
        
    }

    


}