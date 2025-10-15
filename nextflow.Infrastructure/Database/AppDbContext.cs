using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using nextflow.Domain.Models;
using Nextflow.Domain.Models;

namespace nextflow.Infrastructure.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Address> Addresses { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<State> States { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<CategoryProduct> CategoryProducts { get; set; }
    public DbSet<StockMovement> StockMovements { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<User>().HasIndex(u => u.CPF).IsUnique();

        modelBuilder.Entity<Client>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<Client>().HasIndex(u => u.CPF).IsUnique();

        modelBuilder.Entity<Supplier>().HasIndex(u => u.CNPJ).IsUnique();
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information); //tirar depois 
        optionsBuilder.EnableSensitiveDataLogging(); // tirar depois
        base.OnConfiguring(optionsBuilder);
    }
}
