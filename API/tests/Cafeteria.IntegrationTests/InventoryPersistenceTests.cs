using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Cafeteria.Infrastructure.Data;
using Cafeteria.Infrastructure.Repositories;
using Cafeteria.Application.Inventory;

namespace Cafeteria.IntegrationTests;

public class InventoryPersistenceTests
{
    [Fact]
    public async Task Can_Save_And_Retrieve_InventoryItem_With_Movements()
    {
        // use in-memory repository for inventory in this iteration
        var repository = new InMemoryInventoryRepository();
        var service = new InventoryService(repository);

        var request = new CreateInventoryItemRequest(Guid.NewGuid(), 5m, Cafeteria.Domain.Enums.UnitOfMeasure.Unit, 1m, DateTime.UtcNow, null);

        var id = await service.CreateAsync(request);

        var fetched = await repository.GetByIdAsync(id);

        fetched.Should().NotBeNull();
        fetched!.QuantityOnHand.Should().Be(5m);
        fetched.Movements.Should().ContainSingle();
    }
}
