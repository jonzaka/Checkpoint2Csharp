using EfCrudApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCrudApp.Data;

public class AppDbContext : DbContext
{
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();

    private readonly string _dbPath;

    public AppDbContext()
    {
        var basePath = AppContext.BaseDirectory;
        _dbPath = Path.Combine(basePath, "app.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={_dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasMany(c => c.Products)
            .WithOne(p => p.Category!)
            .HasForeignKey(p => p.CategoryId);
    }
}
