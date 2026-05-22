using Cafeteria.Domain.Common;
using Cafeteria.Domain.Enums;
using Cafeteria.Domain.Ingredients;

namespace Cafeteria.Domain.Inventory;

public sealed class InventoryItem : AggregateRoot
{
    private readonly List<InventoryMovement> _movements = new List<InventoryMovement>();

    public Guid IngredientOptionId { get; private set; }
    public decimal QuantityOnHand { get; private set; }
    public UnitOfMeasure UnitOfMeasure { get; private set; }
    public decimal ReorderThreshold { get; private set; }

    public IReadOnlyCollection<InventoryMovement> Movements => _movements.AsReadOnly();

    private InventoryItem()
    {
    }

    public InventoryItem(
        Guid ingredientOptionId,
        decimal initialQuantity,
        UnitOfMeasure unitOfMeasure,
        decimal reorderThreshold,
        DateTime createdAtUtc,
        Guid? createdByUserId)
    {
        if (ingredientOptionId == Guid.Empty)
            throw new ArgumentException("Ingredient option id is required.", nameof(ingredientOptionId));

        if (initialQuantity < 0)
            throw new ArgumentException("Initial quantity cannot be negative.", nameof(initialQuantity));

        if (reorderThreshold < 0)
            throw new ArgumentException("Reorder threshold cannot be negative.", nameof(reorderThreshold));

        IngredientOptionId = ingredientOptionId;
        QuantityOnHand = initialQuantity;
        UnitOfMeasure = unitOfMeasure;
        ReorderThreshold = reorderThreshold;

        if (initialQuantity > 0)
        {
            _movements.Add(InventoryMovement.Create(
                Id,
                InventoryMovementType.StockIn,
                initialQuantity,
                "Initial stock",
                createdAtUtc,
                createdByUserId));
        }
    }

    public void ReceiveStock(decimal quantity, string reason, DateTime createdAtUtc, Guid? createdByUserId)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

        QuantityOnHand += quantity;

        _movements.Add(InventoryMovement.Create(
            Id,
            InventoryMovementType.StockIn,
            quantity,
            reason,
            createdAtUtc,
            createdByUserId));
    }

    public void RemoveStock(decimal quantity, string reason, DateTime createdAtUtc, Guid? createdByUserId)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

        if (quantity > QuantityOnHand)
            throw new InvalidOperationException("Not enough inventory available.");

        QuantityOnHand -= quantity;

        _movements.Add(InventoryMovement.Create(
            Id,
            InventoryMovementType.StockOut,
            quantity,
            reason,
            createdAtUtc,
            createdByUserId));
    }

    public void AdjustQuantity(decimal newQuantity, string reason, DateTime createdAtUtc, Guid? createdByUserId)
    {
        if (newQuantity < 0)
            throw new ArgumentException("New quantity cannot be negative.", nameof(newQuantity));

        QuantityOnHand = newQuantity;

        _movements.Add(InventoryMovement.Create(
            Id,
            InventoryMovementType.Adjustment,
            newQuantity,
            reason,
            createdAtUtc,
            createdByUserId));
    }

    public bool IsLowStock()
    {
        return QuantityOnHand <= ReorderThreshold;
    }
}
