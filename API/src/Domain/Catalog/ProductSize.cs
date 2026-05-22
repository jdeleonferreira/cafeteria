using Cafeteria.Domain.Common;

namespace Cafeteria.Domain.Catalog;

public class ProductSize : Entity
{
    public Guid ProductId { get; private set; }
    public string Name { get; private set; }
    public decimal AdditionalPrice { get; private set; }

    private ProductSize()
    {
        Name = string.Empty;
    }

    public ProductSize(Guid productId, string name, decimal additionalPrice)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Size name is required.", nameof(name));

        if (additionalPrice < 0)
            throw new ArgumentException("Additional price cannot be negative.", nameof(additionalPrice));

        ProductId = productId;
        Name = name.Trim();
        AdditionalPrice = additionalPrice;
    }

    public static ProductSize Regular()
    {
        return new ProductSize(Guid.Empty, "Regular", 0);
    }
}