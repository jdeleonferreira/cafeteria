using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Cafeteria.Application.Orders;
using Cafeteria.Domain.Orders;

namespace Cafeteria.UnitTests.Application.Orders;

public class OrderServiceTests
{
    private class FakeOrderRepository : IOrderRepository
    {
        private readonly List<Order> _store = new();
        public bool SaveCalled { get; private set; }

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
            SaveCalled = true;
            return Task.CompletedTask;
        }
    }

    [Fact]
    public async Task PlaceOrder_CreatesOrderAndReturnsId()
    {
        var repo = new FakeOrderRepository();
        var service = new OrderService(repo);

        var request = new CreateOrderRequest(100, "Alice", 0m, DateTime.UtcNow, new[] { new CreateOrderItemRequest(Guid.NewGuid(), "Coffee", "Regular", 2, 3.5m) });

        var id = await service.PlaceOrderAsync(request);

        id.Should().NotBe(Guid.Empty);
        var created = await repo.GetByIdAsync(id);
        created.Should().NotBeNull();
        created!.GetTotal().Should().Be(7m);
        repo.SaveCalled.Should().BeTrue();
    }

    [Fact]
    public async Task GetById_ReturnsDto()
    {
        var repo = new FakeOrderRepository();
        var order = new Order(1, "Bob", 0m, DateTime.UtcNow);
        order.AddItem(Guid.NewGuid(), "Tea", "Regular", 1, 2m);
        await repo.AddAsync(order);
        await repo.SaveChangesAsync();

        var service = new OrderService(repo);
        var dto = await service.GetByIdAsync(order.Id);

        dto.Should().NotBeNull();
        dto!.Total.Should().Be(2m);
        dto.Items.Should().HaveCount(1);
    }

    [Fact]
    public async Task CancelOrder_SetsCancelled()
    {
        var repo = new FakeOrderRepository();
        var order = new Order(2, "Carol", 0m, DateTime.UtcNow);
        await repo.AddAsync(order);
        await repo.SaveChangesAsync();

        var service = new OrderService(repo);
        await service.CancelOrderAsync(order.Id);

        var fetched = await repo.GetByIdAsync(order.Id);
        fetched.Should().NotBeNull();
        fetched!.Status.ToString().Should().Be("Cancelled");
    }
}
