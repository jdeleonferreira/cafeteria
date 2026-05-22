using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Cafeteria.Infrastructure.Data;
using Cafeteria.Infrastructure.Repositories;
using Cafeteria.Application.Inventory;
using Cafeteria.Application.Orders;
using Cafeteria.Application.Payments;

namespace Cafeteria.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Use InMemory provider for app and tests by default
        services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("CafeteriaDb"));

        // Repositories: use in-memory repositories for stability in early iterations
        services.AddScoped<IInventoryRepository, InMemoryInventoryRepository>();
        services.AddScoped<IOrderRepository, InMemoryOrderRepository>();
        services.AddScoped<IPaymentRepository, InMemoryPaymentRepository>();

        return services;
    }
}
