using System;
using FluentAssertions;
using Xunit;
using Cafeteria.Domain.Catalog;

namespace Cafeteria.UnitTests.Domain;

public class CategoryTests
{
    [Fact]
    public void Create_WithValidData_ShouldSetProperties()
    {
        var category = new Category("Drinks", "Beverages and drinks");

        category.Name.Should().Be("Drinks");
        category.Description.Should().Be("Beverages and drinks");
        category.IsActive.Should().BeTrue();
    }

    [Fact]
    public void Create_WithEmptyName_ShouldThrow()
    {
        Action act = () => new Category("   ");

        act.Should().Throw<ArgumentException>().WithMessage("*Category name is required.*");
    }

    [Fact]
    public void Rename_WithValidName_ShouldUpdateName()
    {
        var category = new Category("Drinks");
        category.Rename("Beverages");

        category.Name.Should().Be("Beverages");
    }

    [Fact]
    public void Rename_WithInvalidName_ShouldThrow()
    {
        var category = new Category("Drinks");
        Action act = () => category.Rename("  ");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Deactivate_And_Activate_ShouldToggleIsActive()
    {
        var category = new Category("Drinks");
        category.Deactivate();
        category.IsActive.Should().BeFalse();
        category.Activate();
        category.IsActive.Should().BeTrue();
    }
}
