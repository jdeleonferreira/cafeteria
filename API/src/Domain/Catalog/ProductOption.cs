using Cafeteria.Domain.Common;

namespace Cafeteria.Domain.Catalog;

public sealed class ProductOption : Entity
{
    public Guid ProductOptionGroupId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public Guid IngredientId { get; private set; }
    public Guid IngredientOptionId { get; private set; }
    public decimal AdditionalPrice { get; private set; }
    public bool IsActive { get; private set; }

    private ProductOption()
    {
    }

    public ProductOption(
        Guid productOptionGroupId,
        string name,
        Guid ingredientId,
        Guid ingredientOptionId,
        decimal additionalPrice)
    {
        if (productOptionGroupId == Guid.Empty)
            throw new ArgumentException("Product option group id is required.", nameof(productOptionGroupId));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product option name is required.", nameof(name));

        if (ingredientId == Guid.Empty)
            throw new ArgumentException("Ingredient id is required.", nameof(ingredientId));

        if (ingredientOptionId == Guid.Empty)
            throw new ArgumentException("Ingredient option id is required.", nameof(ingredientOptionId));

        if (additionalPrice < 0)
            throw new ArgumentException("Additional price cannot be negative.", nameof(additionalPrice));

        ProductOptionGroupId = productOptionGroupId;
        Name = name.Trim();
        IngredientId = ingredientId;
        IngredientOptionId = ingredientOptionId;
        AdditionalPrice = additionalPrice;
        IsActive = true;
    }

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;
}