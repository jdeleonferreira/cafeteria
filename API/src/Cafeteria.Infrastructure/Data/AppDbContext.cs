using Microsoft.EntityFrameworkCore;
using Cafeteria.Domain.Inventory;

namespace Cafeteria.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<InventoryItem> InventoryItems { get; set; } = null!;
    public DbSet<InventoryMovement> InventoryMovements { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new InventoryItemConfiguration());
        modelBuilder.ApplyConfiguration(new InventoryMovementConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
