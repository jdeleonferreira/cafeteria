using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafeteria.Domain.Payments;

namespace Cafeteria.Application.Payments;

public class PaymentService
{
    private readonly IPaymentRepository _repository;

    public PaymentService(IPaymentRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Guid> RegisterPaymentAsync(CreatePaymentRequest request)
    {
        var method = Enum.TryParse<PaymentMethod>(request.Method, true, out var m) ? m : PaymentMethod.Unknown;

        var payment = new Payment(request.OrderId, request.Amount, method, request.TransactionId, request.CreatedAtUtc);

        await _repository.AddAsync(payment);
        await _repository.SaveChangesAsync();

        return payment.Id;
    }

    public async Task MarkCompletedAsync(Guid paymentId, string? transactionId, DateTime completedAtUtc)
    {
        var payment = await _repository.GetByIdAsync(paymentId);
        if (payment == null) throw new InvalidOperationException("Payment not found.");

        payment.MarkCompleted(transactionId, completedAtUtc);
        await _repository.SaveChangesAsync();
    }

    public async Task MarkFailedAsync(Guid paymentId, string reason)
    {
        var payment = await _repository.GetByIdAsync(paymentId);
        if (payment == null) throw new InvalidOperationException("Payment not found.");

        payment.MarkFailed(reason);
        await _repository.SaveChangesAsync();
    }

    public async Task<PaymentDto?> GetByIdAsync(Guid id)
    {
        var p = await _repository.GetByIdAsync(id);
        if (p == null) return null;

        return new PaymentDto(p.Id, p.OrderId, p.Amount, p.Method.ToString(), p.Status.ToString(), p.TransactionId, p.FailureReason, p.CreatedAtUtc, p.CompletedAtUtc);
    }

    public async Task<IReadOnlyList<PaymentDto>> ListAsync()
    {
        var list = await _repository.ListAsync();
        return list.Select(p => new PaymentDto(p.Id, p.OrderId, p.Amount, p.Method.ToString(), p.Status.ToString(), p.TransactionId, p.FailureReason, p.CreatedAtUtc, p.CompletedAtUtc)).ToList();
    }
}
