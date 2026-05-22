using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cafeteria.Domain.Orders;

namespace Cafeteria.Application.Orders;

public interface IOrderRepository
{
    Task AddAsync(Order order);
    Task<Order?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<Order>> ListAsync();
    Task SaveChangesAsync();
}
