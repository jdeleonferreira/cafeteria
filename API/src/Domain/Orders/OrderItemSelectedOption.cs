using Cafeteria.Domain.Common;

namespace Cafeteria.Domain.Orders;

public sealed class OrderItemSelectedOption : Entity
{
    public Guid OrderItemId { get; private set; }
    public string OptionGroupName { get; private set; } = string.Empty;
    public string OptionName { get; private set; } = string.Empty;
    public Guid IngredientId { get; private set; }
    public Guid IngredientOptionId { get; private set; }
    public decimal AdditionalPrice { get; private set; }

    private OrderItemSelectedOption()
    {
    }

    public OrderItemSelectedOption(
        Guid orderItemId,
        string optionGroupName,
        string optionName,
        Guid ingredientId,
        Guid ingredientOptionId,
        decimal additionalPrice)
    {
        if (orderItemId == Guid.Empty)
            throw new ArgumentException("Order item id is required.", nameof(orderItemId));

        if (string.IsNullOrWhiteSpace(optionGroupName))
            throw new ArgumentException("Option group name is required.", nameof(optionGroupName));

        if (string.IsNullOrWhiteSpace(optionName))
            throw new ArgumentException("Option name is required.", nameof(optionName));

        if (ingredientId == Guid.Empty)
            throw new ArgumentException("Ingredient id is required.", nameof(ingredientId));

        if (ingredientOptionId == Guid.Empty)
            throw new ArgumentException("Ingredient option id is required.", nameof(ingredientOptionId));

        if (additionalPrice < 0)
            throw new ArgumentException("Additional price cannot be negative.", nameof(additionalPrice));

        OrderItemId = orderItemId;
        OptionGroupName = optionGroupName.Trim();
        OptionName = optionName.Trim();
        IngredientId = ingredientId;
        IngredientOptionId = ingredientOptionId;
        AdditionalPrice = additionalPrice;
    }
}