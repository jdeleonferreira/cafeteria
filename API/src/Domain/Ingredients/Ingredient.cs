using Cafeteria.Domain.Common;

namespace Cafeteria.Domain.Ingredients;

public class Ingredient : AggregateRoot
{
    private readonly List<IngredientOption> _options = [];

    public string Name { get; private set; }
    public bool IsActive { get; private set; }

    public IReadOnlyCollection<IngredientOption> Options => _options;

    private Ingredient()
    {
        Name = string.Empty;
    }

    public Ingredient(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Ingredient name is required.", nameof(name));

        Name = name.Trim();
        IsActive = true;
    }

    public void AddOption(string name)
    {
        if (_options.Any(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("Ingredient option already exists.");

        _options.Add(new IngredientOption(Id, name));
    }
}