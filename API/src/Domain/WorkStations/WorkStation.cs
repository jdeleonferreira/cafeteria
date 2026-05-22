using Cafeteria.Domain.Common;

namespace Cafeteria.Domain.WorkStations;

public class WorkStation : AggregateRoot
{
    private readonly List<WorkStationIngredientRule> _ingredientRules = [];

    public string Name { get; private set; }
    public bool IsActive { get; private set; }

    public IReadOnlyCollection<WorkStationIngredientRule> IngredientRules => _ingredientRules;

    private WorkStation()
    {
        Name = string.Empty;
    }

    public WorkStation(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Work station name is required.", nameof(name));

        Name = name.Trim();
        IsActive = true;
    }

    public void AssignIngredient(Guid ingredientId)
    {
        if (_ingredientRules.Any(x => x.IngredientId == ingredientId))
            throw new InvalidOperationException("Ingredient already assigned to this work station.");

        _ingredientRules.Add(new WorkStationIngredientRule(Id, ingredientId));
    }
}