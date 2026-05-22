using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cafeteria.Domain.Orders;

namespace Cafeteria.Infrastructure.Data;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(x => x.Id);

        builder.Property<int>("OrderNumber").IsRequired();
        builder.Property(x => x.CustomerName).HasMaxLength(200).IsRequired();
        builder.Property(x => x.DonationAmount).IsRequired();
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.CreatedAtUtc).IsRequired();

        builder.HasMany(typeof(OrderItem), "_items")
            .WithOne()
            .HasForeignKey("OrderId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation("_items").UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}

internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");

        builder.HasKey(x => x.Id);

        builder.Property<Guid>("Id").ValueGeneratedOnAdd();

        builder.Property(x => x.OrderId).IsRequired();
        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.ProductName).HasMaxLength(200).IsRequired();
        builder.Property(x => x.SizeName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.UnitPrice).IsRequired();

        builder.HasMany(typeof(OrderItemSelectedOption), "_selectedOptions")
            .WithOne()
            .HasForeignKey("OrderItemId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation("_selectedOptions").UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}

internal class OrderItemSelectedOptionConfiguration : IEntityTypeConfiguration<OrderItemSelectedOption>
{
    public void Configure(EntityTypeBuilder<OrderItemSelectedOption> builder)
    {
        builder.ToTable("OrderItemSelectedOptions");

        builder.HasKey(x => x.Id);

        builder.Property<Guid>("Id").ValueGeneratedOnAdd();

        builder.Property(x => x.OrderItemId).IsRequired();
        builder.Property(x => x.OptionGroupName).HasMaxLength(200).IsRequired();
        builder.Property(x => x.OptionName).HasMaxLength(200).IsRequired();
        builder.Property(x => x.IngredientId).IsRequired();
        builder.Property(x => x.IngredientOptionId).IsRequired();
        builder.Property(x => x.AdditionalPrice).IsRequired();
    }
}
