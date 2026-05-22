using System;

namespace Cafeteria.Application.Payments;

public record PaymentDto(Guid Id, Guid OrderId, decimal Amount, string Method, string Status, string? TransactionId, string? FailureReason, DateTime CreatedAtUtc, DateTime? CompletedAtUtc);
