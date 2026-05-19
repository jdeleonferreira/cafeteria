using Cafeteria.Domain.Common;

namespace Cafeteria.Domain.Orders;

public sealed class OrderItem : Entity
{
    private readonly List<OrderItemSelectedOption> _selectedOptions = [];

    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; } = string.Empty;
    public string SizeName { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }

    public IReadOnlyCollection<OrderItemSelectedOption> SelectedOptions => _selectedOptions.AsReadOnly();

    private OrderItem()
    {
    }

    public OrderItem(
        Guid orderId,
        Guid productId,
        string productName,
        string sizeName,
        int quantity,
        decimal unitPrice)
    {
        if (orderId == Guid.Empty)
            throw new ArgumentException("Order id is required.", nameof(orderId));

        if (productId == Guid.Empty)
            throw new ArgumentException("Product id is required.", nameof(productId));

        if (string.IsNullOrWhiteSpace(productName))
            throw new ArgumentException("Product name is required.", nameof(productName));

        if (string.IsNullOrWhiteSpace(sizeName))
            throw new ArgumentException("Size name is required.", nameof(sizeName));

        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

        if (unitPrice < 0)
            throw new ArgumentException("Unit price cannot be negative.", nameof(unitPrice));

        OrderId = orderId;
        ProductId = productId;
        ProductName = productName.Trim();
        SizeName = sizeName.Trim();
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public OrderItemSelectedOption AddSelectedOption(
        string optionGroupName,
        string optionName,
        Guid ingredientId,
        Guid ingredientOptionId,
        decimal additionalPrice)
    {
        var option = new OrderItemSelectedOption(
            Id,
            optionGroupName,
            optionName,
            ingredientId,
            ingredientOptionId,
            additionalPrice);

        _selectedOptions.Add(option);

        return option;
    }

    public decimal GetLineTotal()
    {
        var optionsTotal = _selectedOptions.Sum(x => x.AdditionalPrice);
        return (UnitPrice + optionsTotal) * Quantity;
    }
}