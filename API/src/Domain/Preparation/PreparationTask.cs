using Cafeteria.Domain.Common;

namespace Cafeteria.Domain.Preparation;

public sealed class PreparationTask : AggregateRoot
{
    public Guid OrderId { get; private set; }
    public Guid OrderItemId { get; private set; }
    public Guid WorkStationId { get; private set; }
    public Guid IngredientId { get; private set; }
    public Guid IngredientOptionId { get; private set; }

    public int Quantity { get; private set; }
    public PreparationTaskStatus Status { get; private set; }

    public DateTime CreatedAtUtc { get; private set; }
    public DateTime? StartedAtUtc { get; private set; }
    public DateTime? CompletedAtUtc { get; private set; }

    private PreparationTask()
    {
    }

    public PreparationTask(
        Guid orderId,
        Guid orderItemId,
        Guid workStationId,
        Guid ingredientId,
        Guid ingredientOptionId,
        int quantity,
        DateTime createdAtUtc)
    {
        if (orderId == Guid.Empty)
            throw new ArgumentException("Order id is required.", nameof(orderId));

        if (orderItemId == Guid.Empty)
            throw new ArgumentException("Order item id is required.", nameof(orderItemId));

        if (workStationId == Guid.Empty)
            throw new ArgumentException("Work station id is required.", nameof(workStationId));

        if (ingredientId == Guid.Empty)
            throw new ArgumentException("Ingredient id is required.", nameof(ingredientId));

        if (ingredientOptionId == Guid.Empty)
            throw new ArgumentException("Ingredient option id is required.", nameof(ingredientOptionId));

        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

        OrderId = orderId;
        OrderItemId = orderItemId;
        WorkStationId = workStationId;
        IngredientId = ingredientId;
        IngredientOptionId = ingredientOptionId;
        Quantity = quantity;
        CreatedAtUtc = createdAtUtc;
        Status = PreparationTaskStatus.Pending;
    }

    public void Start(DateTime startedAtUtc)
    {
        if (Status != PreparationTaskStatus.Pending)
            throw new InvalidOperationException("Only pending preparation tasks can be started.");

        StartedAtUtc = startedAtUtc;
        Status = PreparationTaskStatus.InProgress;
    }

    public void Complete(DateTime completedAtUtc)
    {
        if (Status != PreparationTaskStatus.InProgress)
            throw new InvalidOperationException("Only in-progress preparation tasks can be completed.");

        CompletedAtUtc = completedAtUtc;
        Status = PreparationTaskStatus.Completed;
    }

    public void Cancel()
    {
        if (Status == PreparationTaskStatus.Completed)
            throw new InvalidOperationException("Completed preparation tasks cannot be cancelled.");

        Status = PreparationTaskStatus.Cancelled;
    }
}