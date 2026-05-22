using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Cafeteria.Infrastructure.Data;
using Cafeteria.Infrastructure.Repositories;
using Cafeteria.Application.Orders;

namespace Cafeteria.IntegrationTests;

public class OrdersPersistenceTests
{
    [Fact]
    public async Task Can_Save_And_Retrieve_Order_With_Items()
    {
        // use in-memory repository for orders in this iteration
        var repo = new InMemoryOrderRepository();
        var service = new OrderService(repo);

        var request = new CreateOrderRequest(200, "Eve", 0m, DateTime.UtcNow, new[] { new CreateOrderItemRequest(Guid.NewGuid(), "Latte", "Regular", 1, 4m) });

        var id = await service.PlaceOrderAsync(request);

        var fetched = await repo.GetByIdAsync(id);

        fetched.Should().NotBeNull();
        fetched!.Items.Should().ContainSingle();
        fetched.GetTotal().Should().Be(4m);
    }
}
