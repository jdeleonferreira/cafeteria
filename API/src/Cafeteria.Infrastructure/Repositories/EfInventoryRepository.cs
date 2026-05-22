using Microsoft.EntityFrameworkCore;
using Cafeteria.Application.Inventory;
using Cafeteria.Domain.Inventory;
using Cafeteria.Infrastructure.Data;

namespace Cafeteria.Infrastructure.Repositories;

public class EfInventoryRepository : IInventoryRepository
{
    private readonly AppDbContext _db;

    public EfInventoryRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(InventoryItem item)
    {
        _db.InventoryItems.Add(item);
        await Task.CompletedTask;
    }

    public async Task<InventoryItem?> GetByIdAsync(Guid id)
    {
        return await _db.InventoryItems.Include(i => i.Movements).FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IReadOnlyList<InventoryItem>> ListAsync()
    {
        return await _db.InventoryItems.Include(i => i.Movements).ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}
