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
}