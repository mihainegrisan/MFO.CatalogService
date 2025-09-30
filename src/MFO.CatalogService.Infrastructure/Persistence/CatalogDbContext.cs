using MFO.CatalogService.Application.Common;
using MFO.CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;

namespace MFO.CatalogService.Infrastructure.Persistence;

public class CatalogDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<SkuSequence> SkuSequences { get; set; }

    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

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

        modelBuilder.Entity<SkuSequence>(entity =>
        {
            entity.HasKey(e => e.SkuSequenceId);

            entity.HasIndex(e => new { e.Company, e.Category, e.Brand })
                .IsUnique();

            ConfigureFixedCode(entity, e => e.Company);
            ConfigureFixedCode(entity, e => e.Category);
            ConfigureFixedCode(entity, e => e.Brand);

            entity.Property(e => e.LastNumber)
                .HasDefaultValue(0)
                .IsRequired();
        });
    }

    private void ConfigureFixedCode(EntityTypeBuilder<SkuSequence> entity, Expression<Func<SkuSequence, string>> property)
    {
        entity.Property(property)
            .HasMaxLength(ValidationConstants.CodeLength)
            .IsFixedLength()
            .IsRequired();
    }
}