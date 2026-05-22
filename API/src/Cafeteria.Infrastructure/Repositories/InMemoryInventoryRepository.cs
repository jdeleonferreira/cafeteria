using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafeteria.Application.Inventory;
using Cafeteria.Domain.Inventory;

namespace Cafeteria.Infrastructure.Repositories;

public class InMemoryInventoryRepository : IInventoryRepository
{
    private readonly List<InventoryItem> _store = new();

    public Task AddAsync(InventoryItem item)
    {
        _store.Add(item);
        return Task.CompletedTask;
    }

    public Task<InventoryItem?> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_store.FirstOrDefault(i => i.Id == id));
    }

    public Task<IReadOnlyList<InventoryItem>> ListAsync()
    {
        return Task.FromResult((IReadOnlyList<InventoryItem>)_store.ToList());
    }

    public Task SaveChangesAsync()
    {
        return Task.CompletedTask;
    }
}
