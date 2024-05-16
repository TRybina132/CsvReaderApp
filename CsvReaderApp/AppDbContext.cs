using CsvReaderApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace CsvReaderApp;

public class AppDbContext : DbContext
{
    private const string ConnectionString = "Server=localhost;Trusted_Connection=True;Database=App;TrustServerCertificate=true;";
    
    public DbSet<VendorEntity> Vendors { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}