using System;
using FluentAssertions;
using Xunit;
using Cafeteria.Domain.Inventory;
using Cafeteria.Domain.Enums;

namespace Cafeteria.UnitTests.Domain.Inventory;

public class InventoryItemTests
{
    [Fact]
    public void Constructor_WithValidInitialQuantity_AddsInitialMovement()
    {
        var optionId = Guid.NewGuid();
        var createdAt = DateTime.UtcNow;
        var item = new InventoryItem(optionId, 10m, UnitOfMeasure.Unit, 2m, createdAt, Guid.NewGuid());

        item.QuantityOnHand.Should().Be(10m);
        item.Movements.Should().NotBeNull();
        item.Movements.Should().ContainSingle();

        var movement = System.Linq.Enumerable.First(item.Movements);
        movement.Type.Should().Be(InventoryMovementType.StockIn);
        movement.Quantity.Should().Be(10m);
    }

    [Fact]
    public void Constructor_WithZeroInitialQuantity_NoInitialMovement()
    {
        var optionId = Guid.NewGuid();
        var item = new InventoryItem(optionId, 0m, UnitOfMeasure.Unit, 2m, DateTime.UtcNow, null);

        item.QuantityOnHand.Should().Be(0m);
        item.Movements.Should().BeEmpty();
    }

    [Fact]
    public void ReceiveStock_IncreasesQuantityAndAddsMovement()
    {
        var optionId = Guid.NewGuid();
        var item = new InventoryItem(optionId, 0m, UnitOfMeasure.Unit, 2m, DateTime.UtcNow, null);

        item.ReceiveStock(5m, "Restock", DateTime.UtcNow, null);

        item.QuantityOnHand.Should().Be(5m);
        item.Movements.Should().ContainSingle(m => m.Type == InventoryMovementType.StockIn && m.Quantity == 5m);
    }

    [Fact]
    public void ReceiveStock_NonPositive_Throws()
    {
        var optionId = Guid.NewGuid();
        var item = new InventoryItem(optionId, 0m, UnitOfMeasure.Unit, 2m, DateTime.UtcNow, null);

        Action act = () => item.ReceiveStock(0m, "", DateTime.UtcNow, null);

        act.Should().Throw<ArgumentException>().WithMessage("*Quantity must be greater than zero.*");
    }

    [Fact]
    public void RemoveStock_DecreasesQuantityAndAddsMovement()
    {
        var optionId = Guid.NewGuid();
        var item = new InventoryItem(optionId, 10m, UnitOfMeasure.Unit, 2m, DateTime.UtcNow, null);

        item.RemoveStock(4m, "Sale", DateTime.UtcNow, null);

        item.QuantityOnHand.Should().Be(6m);
        item.Movements.Should().Contain(m => m.Type == InventoryMovementType.StockOut && m.Quantity == 4m);
    }

    [Fact]
    public void RemoveStock_Insufficient_Throws()
    {
        var optionId = Guid.NewGuid();
        var item = new InventoryItem(optionId, 2m, UnitOfMeasure.Unit, 1m, DateTime.UtcNow, null);

        Action act = () => item.RemoveStock(5m, "Sale", DateTime.UtcNow, null);

        act.Should().Throw<InvalidOperationException>().WithMessage("*Not enough inventory available.*");
    }

    [Fact]
    public void AdjustQuantity_SetsQuantityAndAddsAdjustmentMovement()
    {
        var optionId = Guid.NewGuid();
        var item = new InventoryItem(optionId, 5m, UnitOfMeasure.Unit, 1m, DateTime.UtcNow, null);

        item.AdjustQuantity(3m, "Cycle count", DateTime.UtcNow, null);

        item.QuantityOnHand.Should().Be(3m);
        item.Movements.Should().Contain(m => m.Type == InventoryMovementType.Adjustment && m.Quantity == 3m);
    }

    [Fact]
    public void AdjustQuantity_Negative_Throws()
    {
        var optionId = Guid.NewGuid();
        var item = new InventoryItem(optionId, 5m, UnitOfMeasure.Unit, 1m, DateTime.UtcNow, null);

        Action act = () => item.AdjustQuantity(-1m, "", DateTime.UtcNow, null);

        act.Should().Throw<ArgumentException>().WithMessage("*New quantity cannot be negative.*");
    }

    [Fact]
    public void IsLowStock_ReturnsTrue_WhenUnderOrEqualThreshold()
    {
        var optionId = Guid.NewGuid();
        var item = new InventoryItem(optionId, 2m, UnitOfMeasure.Unit, 2m, DateTime.UtcNow, null);

        item.IsLowStock().Should().BeTrue();

        item.RemoveStock(1m, "Sale", DateTime.UtcNow, null);
        item.IsLowStock().Should().BeTrue();
    }

    [Fact]
    public void IsLowStock_ReturnsFalse_WhenAboveThreshold()
    {
        var optionId = Guid.NewGuid();
        var item = new InventoryItem(optionId, 5m, UnitOfMeasure.Unit, 2m, DateTime.UtcNow, null);

        item.IsLowStock().Should().BeFalse();
    }
}
