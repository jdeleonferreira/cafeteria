using Cafeteria.Domain.Common;

namespace Cafeteria.Domain.WorkStations;

public sealed class WorkStationIngredientRule : Entity
{
    public Guid WorkStationId { get; private set; }
    public Guid IngredientId { get; private set; }

    private WorkStationIngredientRule()
    {
    }

    public WorkStationIngredientRule(Guid workStationId, Guid ingredientId)
    {
        if (workStationId == Guid.Empty)
            throw new ArgumentException("Work station id is required.", nameof(workStationId));

        if (ingredientId == Guid.Empty)
            throw new ArgumentException("Ingredient id is required.", nameof(ingredientId));

        WorkStationId = workStationId;
        IngredientId = ingredientId;
    }
}