using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafeteria.Application.Payments;
using Cafeteria.Domain.Payments;

namespace Cafeteria.Infrastructure.Repositories;

public class InMemoryPaymentRepository : IPaymentRepository
{
    private readonly List<Payment> _store = new();

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
        return Task.CompletedTask;
    }
}
