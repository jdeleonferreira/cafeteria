using Cafeteria.Domain.Common;

namespace Cafeteria.Domain.Payments;

public sealed class Payment : AggregateRoot
{
    public Guid OrderId { get; private set; }
    public decimal Amount { get; private set; }
    public PaymentMethod Method { get; private set; }
    public PaymentStatus Status { get; private set; }
    public string? TransactionId { get; private set; }
    public string? FailureReason { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime? CompletedAtUtc { get; private set; }

    private Payment()
    {
    }

    public Payment(Guid orderId, decimal amount, PaymentMethod method, string? transactionId, DateTime createdAtUtc)
    {
        if (orderId == Guid.Empty)
            throw new ArgumentException("Order id is required.", nameof(orderId));

        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.", nameof(amount));

        OrderId = orderId;
        Amount = amount;
        Method = method;
        TransactionId = string.IsNullOrWhiteSpace(transactionId) ? null : transactionId?.Trim();
        CreatedAtUtc = createdAtUtc;
        Status = PaymentStatus.Pending;
    }

    public void MarkCompleted(string? transactionId, DateTime completedAtUtc)
    {
        if (Status != PaymentStatus.Pending)
            throw new InvalidOperationException("Only pending payments can be marked as completed.");

        if (!string.IsNullOrWhiteSpace(transactionId))
            TransactionId = transactionId!.Trim();

        CompletedAtUtc = completedAtUtc;
        Status = PaymentStatus.Completed;
    }

    public void MarkFailed(string reason)
    {
        if (Status == PaymentStatus.Completed)
            throw new InvalidOperationException("Completed payments cannot be marked as failed.");

        FailureReason = string.IsNullOrWhiteSpace(reason) ? "No reason provided" : reason.Trim();
        Status = PaymentStatus.Failed;
    }
}
