using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cafeteria.Domain.Payments;

namespace Cafeteria.Application.Payments;

public interface IPaymentRepository
{
    Task AddAsync(Payment payment);
    Task<Payment?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<Payment>> ListAsync();
    Task SaveChangesAsync();
}
