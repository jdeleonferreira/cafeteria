using Cafeteria.Domain.Common;

namespace Cafeteria.Domain.Catalog;

public sealed class ProductCategory : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }

    private ProductCategory()
    {
    }

    public ProductCategory(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name is required.", nameof(name));

        Name = name.Trim();
        IsActive = true;
    }

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;
}