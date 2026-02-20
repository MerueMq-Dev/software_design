using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Project.Storage.Entities;

namespace Project.Storage;

public class ProjectDbContext : DbContext
{
    public ProjectDbContext() { }

    private readonly string _connectionString;

    public ProjectDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
        modelBuilder.Entity<item>(entity =>
        {
            entity.ToTable(nameof(item));

            entity.HasKey(e => e.id);
            entity.Property(e => e.id)
                .ValueGeneratedOnAdd()
                .HasColumnType("integer");
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
       => options.UseNpgsql(_connectionString);

}
