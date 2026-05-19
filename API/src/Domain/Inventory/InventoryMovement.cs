using Cafeteria.Domain.Common;

namespace Cafeteria.Domain.Inventory;

public sealed class InventoryMovement : Entity
{
    public Guid InventoryItemId { get; private set; }
    public InventoryMovementType Type { get; private set; }
    public decimal Quantity { get; private set; }
    public string Reason { get; private set; } = string.Empty;
    public DateTime CreatedAtUtc { get; private set; }
    public Guid? CreatedByUserId { get; private set; }

    private InventoryMovement()
    {
    }

    private InventoryMovement(
        Guid inventoryItemId,
        InventoryMovementType type,
        decimal quantity,
        string reason,
        DateTime createdAtUtc,
        Guid? createdByUserId)
    {
        if (inventoryItemId == Guid.Empty)
            throw new ArgumentException("Inventory item id is required.", nameof(inventoryItemId));

        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

        InventoryItemId = inventoryItemId;
        Type = type;
        Quantity = quantity;
        Reason = string.IsNullOrWhiteSpace(reason) ? "No reason provided" : reason.Trim();
        CreatedAtUtc = createdAtUtc;
        CreatedByUserId = createdByUserId;
    }

    public static InventoryMovement Create(
        Guid inventoryItemId,
        InventoryMovementType type,
        decimal quantity,
        string reason,
        DateTime createdAtUtc,
        Guid? createdByUserId)
    {
        return new InventoryMovement(
            inventoryItemId,
            type,
            quantity,
            reason,
            createdAtUtc,
            createdByUserId);
    }
}