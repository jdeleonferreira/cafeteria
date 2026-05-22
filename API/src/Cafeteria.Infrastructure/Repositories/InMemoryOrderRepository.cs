using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafeteria.Application.Orders;
using Cafeteria.Domain.Orders;

namespace Cafeteria.Infrastructure.Repositories;

public class InMemoryOrderRepository : IOrderRepository
{
    private readonly List<Order> _store = new();

    public Task AddAsync(Order order)
    {
        _store.Add(order);
        return Task.CompletedTask;
    }

    public Task<Order?> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_store.FirstOrDefault(o => o.Id == id));
    }

    public Task<IReadOnlyList<Order>> ListAsync()
    {
        return Task.FromResult((IReadOnlyList<Order>)_store.ToList());
    }

    public Task SaveChangesAsync()
    {
        return Task.CompletedTask;
    }
}
