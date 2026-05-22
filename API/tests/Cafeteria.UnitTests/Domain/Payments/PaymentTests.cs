using System;
using FluentAssertions;
using Xunit;
using Cafeteria.Domain.Payments;

namespace Cafeteria.UnitTests.Domain.Payments;

public class PaymentTests
{
    [Fact]
    public void Create_WithValidData_SetsProperties()
    {
        var orderId = Guid.NewGuid();
        var createdAt = DateTime.UtcNow;

        var p = new Payment(orderId, 10m, PaymentMethod.Card, "tx-123", createdAt);

        p.OrderId.Should().Be(orderId);
        p.Amount.Should().Be(10m);
        p.Method.Should().Be(PaymentMethod.Card);
        p.Status.Should().Be(PaymentStatus.Pending);
        p.TransactionId.Should().Be("tx-123");
        p.CreatedAtUtc.Should().Be(createdAt);
    }

    [Fact]
    public void Create_WithInvalidOrderId_Throws()
    {
        Action act = () => new Payment(Guid.Empty, 10m, PaymentMethod.Cash, null, DateTime.UtcNow);

        act.Should().Throw<ArgumentException>().WithMessage("*Order id is required.*");
    }

    [Fact]
    public void Create_WithNonPositiveAmount_Throws()
    {
        Action act = () => new Payment(Guid.NewGuid(), 0m, PaymentMethod.Cash, null, DateTime.UtcNow);

        act.Should().Throw<ArgumentException>().WithMessage("*Amount must be greater than zero.*");
    }

    [Fact]
    public void MarkCompleted_FromPending_SetsCompleted()
    {
        var p = new Payment(Guid.NewGuid(), 5m, PaymentMethod.Mobile, null, DateTime.UtcNow);

        p.MarkCompleted("tx-999", DateTime.UtcNow);

        p.Status.Should().Be(PaymentStatus.Completed);
        p.TransactionId.Should().Be("tx-999");
        p.CompletedAtUtc.Should().NotBeNull();
    }

    [Fact]
    public void MarkCompleted_FromNonPending_Throws()
    {
        var p = new Payment(Guid.NewGuid(), 5m, PaymentMethod.Mobile, null, DateTime.UtcNow);
        p.MarkFailed("Insufficient funds");

        Action act = () => p.MarkCompleted("tx-1", DateTime.UtcNow);

        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void MarkFailed_SetsFailed()
    {
        var p = new Payment(Guid.NewGuid(), 5m, PaymentMethod.Cash, null, DateTime.UtcNow);

        p.MarkFailed("Network error");

        p.Status.Should().Be(PaymentStatus.Failed);
        p.FailureReason.Should().Be("Network error");
    }

    [Fact]
    public void MarkFailed_WhenCompleted_Throws()
    {
        var p = new Payment(Guid.NewGuid(), 5m, PaymentMethod.Cash, null, DateTime.UtcNow);
        p.MarkCompleted("tx-ok", DateTime.UtcNow);

        Action act = () => p.MarkFailed("x");

        act.Should().Throw<InvalidOperationException>().WithMessage("*Completed payments cannot be marked as failed.*");
    }
}
