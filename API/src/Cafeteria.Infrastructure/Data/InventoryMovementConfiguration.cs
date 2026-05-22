using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cafeteria.Domain.Inventory;

namespace Cafeteria.Infrastructure.Data;

internal class InventoryMovementConfiguration : IEntityTypeConfiguration<InventoryMovement>
{
    public void Configure(EntityTypeBuilder<InventoryMovement> builder)
    {
        builder.ToTable("InventoryMovements");

        builder.HasKey(x => x.Id);
        builder.Property<Guid>("Id").ValueGeneratedOnAdd();

        builder.Property(x => x.InventoryItemId).IsRequired();
        builder.Property(x => x.Type).IsRequired();
        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.Reason).HasMaxLength(500).IsRequired();
        builder.Property(x => x.CreatedAtUtc).IsRequired();
        builder.Property(x => x.CreatedByUserId);
    }
}
