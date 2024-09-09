using Microsoft.EntityFrameworkCore;
using service_infrastructure.entities;

namespace service_infrastructure.infrastructure.database;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<UserAccount> UserAccounts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserAccount>().ToTable("UserAccount");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Set the connection string for SQL Server
            optionsBuilder.UseSqlServer("Server=localhost,1433;Initial Catalog=master;Persist Security Info=False;User ID=sa;Password=StrongPassword1;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;");
        }
    }
}