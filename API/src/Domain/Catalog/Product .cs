using Cafeteria.Domain.Common;

namespace Cafeteria.Domain.Catalog;

public class Product : AggregateRoot
{
    private readonly List<ProductSize> _availableSizes = [];
    private readonly List<ProductIngredientRule> _ingredientRules = [];
    private readonly List<ProductOptionGroup> _optionGroups = [];

    public string Name { get; private set; }
    public string Description { get; private set; }
    public Guid CategoryId { get; private set; }
    public decimal BasePrice { get; private set; }
    public bool IsActive { get; private set; }

    public IReadOnlyCollection<ProductSize> AvailableSizes => _availableSizes;
    public IReadOnlyCollection<ProductIngredientRule> IngredientRules => _ingredientRules;
    public IReadOnlyCollection<ProductOptionGroup> OptionGroups => _optionGroups;

    private Product()
    {
        Name = string.Empty;
        Description = string.Empty;
    }

    public Product(string name, string description, Guid categoryId, decimal basePrice)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name is required.", nameof(name));

        if (basePrice < 0)
            throw new ArgumentException("Base price cannot be negative.", nameof(basePrice));

        Name = name.Trim();
        Description = description.Trim();
        CategoryId = categoryId;
        BasePrice = basePrice;
        IsActive = true;

        _availableSizes.Add(ProductSize.Regular());
    }

    public void AddSize(string name, decimal additionalPrice = 0)
    {
        if (_availableSizes.Any(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("Product size already exists.");

        _availableSizes.Add(new ProductSize(Id, name, additionalPrice));
    }

    public void AddIngredientRule(Guid ingredientId, Guid? defaultIngredientOptionId, Guid workStationId, bool isRequired)
    {
        _ingredientRules.Add(new ProductIngredientRule(Id, ingredientId, defaultIngredientOptionId, workStationId, isRequired));
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }
}