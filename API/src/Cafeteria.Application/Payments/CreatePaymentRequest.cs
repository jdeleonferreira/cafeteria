using System;

namespace Cafeteria.Application.Payments;

public record CreatePaymentRequest(Guid OrderId, decimal Amount, string Method, DateTime CreatedAtUtc, string? TransactionId);
