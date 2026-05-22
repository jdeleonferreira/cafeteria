using System;
using FluentAssertions;
using Xunit;
using Cafeteria.Domain.Orders;

namespace Cafeteria.UnitTests.Domain.Orders;

public class OrderTests
{
    [Fact]
    public void Create_WithValidData_SetsProperties()
    {
        var order = new Order(1, "John", 2.5m, DateTime.UtcNow);

        order.OrderNumber.Should().Be(1);
        order.CustomerName.Should().Be("John");
        order.DonationAmount.Should().Be(2.5m);
        order.Status.Should().Be(Cafeteria.Domain.Enums.OrderStatus.Confirmed);
    }

    [Fact]
    public void AddItem_AddsOrderItem()
    {
        var order = new Order(1, "John", 0m, DateTime.UtcNow);
        var productId = Guid.NewGuid();

        order.AddItem(productId, "Coffee", "Regular", 2, 3.5m);

        order.Items.Should().HaveCount(1);
        order.GetTotal().Should().Be(7m);
    }

    [Fact]
    public void GetTotal_IncludesDonation()
    {
        var order = new Order(1, "Jane", 1.5m, DateTime.UtcNow);
        var productId = Guid.NewGuid();

        order.AddItem(productId, "Tea", "Regular", 1, 2m);

        order.GetTotal().Should().Be(3.5m);
    }

    [Fact]
    public void Cancel_Delivered_Throws()
    {
        var order = new Order(1, "John", 0m, DateTime.UtcNow);
        order.MarkInPreparation();
        order.MarkReady();
        order.MarkDelivered();

        Action act = () => order.Cancel();

        act.Should().Throw<InvalidOperationException>().WithMessage("*Delivered orders cannot be cancelled.*");
    }

    [Fact]
    public void Cancel_WhenNotDelivered_SetsCancelled()
    {
        var order = new Order(1, "John", 0m, DateTime.UtcNow);

        order.Cancel();

        order.Status.Should().Be(Cafeteria.Domain.Enums.OrderStatus.Cancelled);
    }
}
