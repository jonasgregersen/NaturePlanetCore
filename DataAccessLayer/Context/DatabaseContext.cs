using DataAccessLayer.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DALOrder = DataAccessLayer.Model.Order;
using DALProduct = DataAccessLayer.Model.DALProduct;

namespace DataAccessLayer.Context
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<DALProduct> Products { get; set; } = null!;
        public DbSet<DALOrder> Orders { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Identity først
            base.OnModelCreating(modelBuilder);

            // PRODUCT
            modelBuilder.Entity<DALProduct>(entity =>
            {
                entity.HasKey(p => p.ProductId);

                entity.Property(p => p.ProductId)
                    .HasColumnName("ProductID")
                    .HasMaxLength(50)
                    .HasColumnType("varchar(50)")
                    .ValueGeneratedNever();

                entity.Property(p => p.Name).HasColumnName("Name").HasMaxLength(200);
                entity.Property(p => p.EAN).HasColumnName("EAN").HasMaxLength(50);
                entity.Property(p => p.ERP_Source).HasColumnName("ERP_Source").HasMaxLength(100);
                entity.Property(p => p.Segment).HasColumnName("Segment").HasMaxLength(100);

                entity.Property(p => p.Product_Category_1).HasColumnName("Product_Category_1").HasMaxLength(100);
                entity.Property(p => p.Product_Category_2).HasColumnName("Product_Category_2").HasMaxLength(100);
                entity.Property(p => p.Product_Category_3).HasColumnName("Product_Category_3").HasMaxLength(100);

                entity.Property(p => p.Weight)
                    .HasColumnName("Weight")
                    .HasColumnType("decimal(18,4)")
                    .HasDefaultValue(0m);
            });

            // ORDER
            modelBuilder.Entity<DALOrder>(entity =>
            {
                entity.HasKey(o => o.OrderId);
                entity.Property(o => o.OrderId).ValueGeneratedOnAdd();

                entity.HasOne(o => o.User)
                    .WithMany(u => u.Orders)
                    .HasForeignKey(o => o.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<DALOrder>()
                .HasMany(o => o.Products)
                .WithMany(p => p.Orders)
                .UsingEntity<Dictionary<string, object>>(
                    "OrderProducts",

                    j => j
                        .HasOne<DALProduct>()
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .HasPrincipalKey(p => p.ProductId)
                        .OnDelete(DeleteBehavior.Cascade),

                    j => j
                        .HasOne<DALOrder>()
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .HasPrincipalKey(o => o.OrderId)
                        .OnDelete(DeleteBehavior.Cascade),

                    j =>
                    {
                        j.HasKey("OrderId", "ProductId");

                        j.ToTable("OrderProducts");

                        j.Property<string>("ProductId")
                            .HasColumnType("varchar(50)");

                        j.Property<int>("OrderId")
                            .HasColumnType("int");
                    }
                );
        }
    }
}
