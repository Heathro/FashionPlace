using Microsoft.EntityFrameworkCore;
using CatalogService.Entities;
using MassTransit;

namespace CatalogService.Data;

public class CatalogDbContext : DbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Specification> Specifications { get; set; }
    public DbSet<SpecificationType> SpecificationTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Model)
            .WithOne(m => m.Product)
            .HasForeignKey<Product>(p => p.ModelId)
            .OnDelete(DeleteBehavior.Cascade);
            
        modelBuilder.Entity<Model>()
            .HasOne(m => m.Brand)
            .WithMany(b => b.Models)
            .HasForeignKey(m => m.BrandId);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

        modelBuilder.Entity<Specification>()
            .HasOne(s => s.Product)
            .WithMany(p => p.Specifications)
            .HasForeignKey(s => s.ProductId);

        modelBuilder.Entity<Specification>()
            .HasOne(s => s.SpecificationType)
            .WithMany(st => st.Specifications)
            .HasForeignKey(s => s.SpecificationTypeId);
    }
}
