using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Cafeteria.Application.Payments;
using Cafeteria.Domain.Payments;

namespace Cafeteria.UnitTests.Application.Payments;

public class PaymentServiceTests
{
    private class FakePaymentRepository : IPaymentRepository
    {
        private readonly List<Payment> _store = new();
        public bool SaveCalled { get; private set; }

        public Task AddAsync(Payment payment)
        {
            _store.Add(payment);
            return Task.CompletedTask;
        }

        public Task<Payment?> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_store.FirstOrDefault(p => p.Id == id));
        }

        public Task<IReadOnlyList<Payment>> ListAsync()
        {
            return Task.FromResult((IReadOnlyList<Payment>)_store.ToList());
        }

        public Task SaveChangesAsync()
        {
            SaveCalled = true;
            return Task.CompletedTask;
        }
    }

    [Fact]
    public async Task RegisterPayment_CreatesPaymentAndReturnsId()
    {
        var repo = new FakePaymentRepository();
        var service = new PaymentService(repo);

        var request = new CreatePaymentRequest(Guid.NewGuid(), 12.5m, "Card", DateTime.UtcNow, "tx-1");

        var id = await service.RegisterPaymentAsync(request);

        id.Should().NotBe(Guid.Empty);
        var created = await repo.GetByIdAsync(id);
        created.Should().NotBeNull();
        created!.Amount.Should().Be(12.5m);
        repo.SaveCalled.Should().BeTrue();
    }

    [Fact]
    public async Task MarkCompleted_ChangesStatus()
    {
        var repo = new FakePaymentRepository();
        var payment = new Payment(Guid.NewGuid(), 5m, PaymentMethod.Cash, null, DateTime.UtcNow);
        await repo.AddAsync(payment);
        await repo.SaveChangesAsync();

        var service = new PaymentService(repo);
        await service.MarkCompletedAsync(payment.Id, "tx-ok", DateTime.UtcNow);

        var fetched = await repo.GetByIdAsync(payment.Id);
        fetched.Should().NotBeNull();
        fetched!.Status.Should().Be(PaymentStatus.Completed);
        fetched.TransactionId.Should().Be("tx-ok");
    }

    [Fact]
    public async Task MarkFailed_ChangesStatus()
    {
        var repo = new FakePaymentRepository();
        var payment = new Payment(Guid.NewGuid(), 5m, PaymentMethod.Cash, null, DateTime.UtcNow);
        await repo.AddAsync(payment);
        await repo.SaveChangesAsync();

        var service = new PaymentService(repo);
        await service.MarkFailedAsync(payment.Id, "Card declined");

        var fetched = await repo.GetByIdAsync(payment.Id);
        fetched.Should().NotBeNull();
        fetched!.Status.Should().Be(PaymentStatus.Failed);
        fetched.FailureReason.Should().Be("Card declined");
    }
}
