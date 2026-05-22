using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cafeteria.Application.Orders;
using Cafeteria.Domain.Orders;
using Cafeteria.Infrastructure.Data;

namespace Cafeteria.Infrastructure.Repositories;

public class EfOrderRepository : IOrderRepository
{
    private readonly AppDbContext _db;

    public EfOrderRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Order order)
    {
        // Ensure database is created (useful for InMemory provider during tests)
        await _db.Database.EnsureCreatedAsync();
        _db.Orders.Add(order);
        await Task.CompletedTask;
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _db.Orders.Include(o => o.Items).ThenInclude(i => i.SelectedOptions).FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<IReadOnlyList<Order>> ListAsync()
    {
        return await _db.Orders.Include(o => o.Items).ThenInclude(i => i.SelectedOptions).ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}
