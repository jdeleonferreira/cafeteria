using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cafeteria.Domain.Inventory;

namespace Cafeteria.Infrastructure.Data;

internal class InventoryItemConfiguration : IEntityTypeConfiguration<InventoryItem>
{
    public void Configure(EntityTypeBuilder<InventoryItem> builder)
    {
        builder.ToTable("InventoryItems");

        builder.HasKey(x => x.Id);

        builder.Property<Guid>("Id").ValueGeneratedNever();

        builder.Property(x => x.IngredientOptionId).IsRequired();
        builder.Property(x => x.QuantityOnHand).IsRequired();
        builder.Property(x => x.UnitOfMeasure).IsRequired();
        builder.Property(x => x.ReorderThreshold).IsRequired();

        builder.HasMany(typeof(InventoryMovement), "Movements")
            .WithOne()
            .HasForeignKey("InventoryItemId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation("Movements").UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
