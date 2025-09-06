using MFO.CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MFO.CatalogService.Infrastructure.Persistence;

public class CatalogDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Brand> Brands { get; set; }

    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasIndex(p => p.SKU)
            .IsUnique()
            .HasDatabaseName("IX_Product_SKU");

        modelBuilder.Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique()
            .HasDatabaseName("IX_Category_Name");

        modelBuilder.Entity<Brand>()
            .HasIndex(b => b.Name)
            .IsUnique()
            .HasDatabaseName("IX_Brand_Name");
    }
}