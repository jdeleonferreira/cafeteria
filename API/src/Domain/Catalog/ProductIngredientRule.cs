using Cafeteria.Domain.Common;

namespace Cafeteria.Domain.Catalog;

public sealed class ProductIngredientRule : Entity
{
    public Guid ProductId { get; private set; }
    public Guid IngredientId { get; private set; }
    public Guid? DefaultIngredientOptionId { get; private set; }
    public Guid WorkStationId { get; private set; }
    public bool IsRequired { get; private set; }

    private ProductIngredientRule()
    {
    }

    public ProductIngredientRule(
        Guid productId,
        Guid ingredientId,
        Guid? defaultIngredientOptionId,
        Guid workStationId,
        bool isRequired)
    {
        if (productId == Guid.Empty)
            throw new ArgumentException("Product id is required.", nameof(productId));

        if (ingredientId == Guid.Empty)
            throw new ArgumentException("Ingredient id is required.", nameof(ingredientId));

        if (workStationId == Guid.Empty)
            throw new ArgumentException("Work station id is required.", nameof(workStationId));

        ProductId = productId;
        IngredientId = ingredientId;
        DefaultIngredientOptionId = defaultIngredientOptionId;
        WorkStationId = workStationId;
        IsRequired = isRequired;
    }
}