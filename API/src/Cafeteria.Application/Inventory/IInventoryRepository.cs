using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cafeteria.Domain.Inventory;

namespace Cafeteria.Application.Inventory;

public interface IInventoryRepository
{
    Task AddAsync(InventoryItem item);
    Task<InventoryItem?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<InventoryItem>> ListAsync();
    Task SaveChangesAsync();
}
