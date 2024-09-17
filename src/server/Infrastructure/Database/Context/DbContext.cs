using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Database.Context;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // applies all entity configurations specified in types implementing IEntityTypeConfiguration
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
}