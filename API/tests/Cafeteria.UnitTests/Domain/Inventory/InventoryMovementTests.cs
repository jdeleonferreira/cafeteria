using System;
using FluentAssertions;
using Xunit;
using Cafeteria.Domain.Inventory;

namespace Cafeteria.UnitTests.Domain.Inventory;

public class InventoryMovementTests
{
    [Fact]
    public void Create_WithValidData_CreatesMovement()
    {
        var inventoryItemId = Guid.NewGuid();
        var m = InventoryMovement.Create(inventoryItemId, InventoryMovementType.StockIn, 10m, "Initial", DateTime.UtcNow, null);

        m.InventoryItemId.Should().Be(inventoryItemId);
        m.Type.Should().Be(InventoryMovementType.StockIn);
        m.Quantity.Should().Be(10m);
        m.Reason.Should().Be("Initial");
    }

    [Fact]
    public void Create_WithEmptyInventoryItemId_Throws()
    {
        Action act = () => InventoryMovement.Create(Guid.Empty, InventoryMovementType.StockIn, 1m, "x", DateTime.UtcNow, null);

        act.Should().Throw<ArgumentException>().WithMessage("*Inventory item id is required.*");
    }

    [Fact]
    public void Create_WithNonPositiveQuantity_Throws()
    {
        Action act = () => InventoryMovement.Create(Guid.NewGuid(), InventoryMovementType.StockIn, 0m, "x", DateTime.UtcNow, null);

        act.Should().Throw<ArgumentException>().WithMessage("*Quantity must be greater than zero.*");
    }

    [Fact]
    public void Create_WithEmptyReason_DefaultsReasonText()
    {
        var m = InventoryMovement.Create(Guid.NewGuid(), InventoryMovementType.Waste, 1m, "   ", DateTime.UtcNow, null);
        m.Reason.Should().Be("No reason provided");
    }
}
