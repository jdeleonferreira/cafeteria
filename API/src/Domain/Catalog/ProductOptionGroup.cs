using Cafeteria.Domain.Common;

namespace Cafeteria.Domain.Catalog;

public sealed class ProductOptionGroup : Entity
{
    private readonly List<ProductOption> _options = [];

    public Guid ProductId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public bool IsRequired { get; private set; }
    public int MinSelections { get; private set; }
    public int MaxSelections { get; private set; }

    public IReadOnlyCollection<ProductOption> Options => _options.AsReadOnly();

    private ProductOptionGroup()
    {
    }

    public ProductOptionGroup(
        Guid productId,
        string name,
        bool isRequired,
        int minSelections,
        int maxSelections)
    {
        if (productId == Guid.Empty)
            throw new ArgumentException("Product id is required.", nameof(productId));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Option group name is required.", nameof(name));

        if (minSelections < 0)
            throw new ArgumentException("Minimum selections cannot be negative.", nameof(minSelections));

        if (maxSelections <= 0)
            throw new ArgumentException("Maximum selections must be greater than zero.", nameof(maxSelections));

        if (minSelections > maxSelections)
            throw new ArgumentException("Minimum selections cannot be greater than maximum selections.");

        ProductId = productId;
        Name = name.Trim();
        IsRequired = isRequired;
        MinSelections = minSelections;
        MaxSelections = maxSelections;
    }

    public ProductOption AddOption(
        string name,
        Guid ingredientId,
        Guid ingredientOptionId,
        decimal additionalPrice)
    {
        if (_options.Any(x => x.Name.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("Product option already exists.");

        var option = new ProductOption(Id, name, ingredientId, ingredientOptionId, additionalPrice);
        _options.Add(option);

        return option;
    }
}