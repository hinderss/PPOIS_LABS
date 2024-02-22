using Microsoft.EntityFrameworkCore;
using Minimarket;

public class MarketDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=mydatabase2.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasNoKey();
        modelBuilder.Entity<Product>().ToTable("Products");

        // Другие настройки сущности Product, если необходимо
    }
}
