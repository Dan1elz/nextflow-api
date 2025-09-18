using Microsoft.EntityFrameworkCore;
using nextflow.Domain.Models;
using Nextflow.Domain.Models;

namespace nextflow.Infrastructure.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Country> Countries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<User>().HasIndex(u => u.CPF).IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}
