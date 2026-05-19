using Cafeteria.Domain.Common;

namespace Cafeteria.Domain.Ingredients;

public class IngredientOption : Entity
{
    public Guid IngredientId { get; private set; }
    public string Name { get; private set; }
    public bool IsActive { get; private set; }

    private IngredientOption()
    {
        Name = string.Empty;
    }

    public IngredientOption(Guid ingredientId, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Ingredient option name is required.", nameof(name));

        IngredientId = ingredientId;
        Name = name.Trim();
        IsActive = true;
    }
}