using Microsoft.EntityFrameworkCore;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<User>().HasIndex(u => u.CPF).IsUnique();
        
        modelBuilder.Entity<Client>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<Client>().HasIndex(u => u.CPF).IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}
