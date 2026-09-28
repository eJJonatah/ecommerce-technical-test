using Microsoft.EntityFrameworkCore;
using TEcomerc.Domain.Entities;

namespace TEcomerc.Infraestructure.Sqlite.Configurations;


public sealed class ApplicationDbContext : DbContext
{
    public DbSet<OrderEntity> Orders => Set<OrderEntity>();
    public DbSet<OrderItemEntity> OrderItems => Set<OrderItemEntity>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _= modelBuilder?.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly);
    }
}