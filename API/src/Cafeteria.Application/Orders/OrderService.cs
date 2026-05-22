using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafeteria.Domain.Orders;

namespace Cafeteria.Application.Orders;

public class OrderService
{
    private readonly IOrderRepository _repository;

    public OrderService(IOrderRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Guid> PlaceOrderAsync(CreateOrderRequest request)
    {
        var order = new Order(request.OrderNumber, request.CustomerName, request.DonationAmount, request.CreatedAtUtc);

        foreach (var it in request.Items ?? Array.Empty<CreateOrderItemRequest>())
        {
            order.AddItem(it.ProductId, it.ProductName, it.SizeName, it.Quantity, it.UnitPrice);
        }

        await _repository.AddAsync(order);
        await _repository.SaveChangesAsync();

        return order.Id;
    }

    public async Task<OrderDto?> GetByIdAsync(Guid id)
    {
        var order = await _repository.GetByIdAsync(id);
        if (order == null) return null;

        var items = order.Items.Select(i => new OrderItemDto(i.Id, i.ProductId, i.ProductName, i.SizeName, i.Quantity, i.UnitPrice, i.GetLineTotal())).ToList().AsReadOnly();

        return new OrderDto(order.Id, order.OrderNumber, order.CustomerName, order.DonationAmount, order.GetTotal(), order.Status.ToString(), order.CreatedAtUtc, items);
    }

    public async Task<IReadOnlyList<OrderDto>> ListAsync()
    {
        var orders = await _repository.ListAsync();
        return orders.Select(o =>
        {
            var items = o.Items.Select(i => new OrderItemDto(i.Id, i.ProductId, i.ProductName, i.SizeName, i.Quantity, i.UnitPrice, i.GetLineTotal())).ToList().AsReadOnly();
            return new OrderDto(o.Id, o.OrderNumber, o.CustomerName, o.DonationAmount, o.GetTotal(), o.Status.ToString(), o.CreatedAtUtc, items);
        }).ToList();
    }

    public async Task CancelOrderAsync(Guid id)
    {
        var order = await _repository.GetByIdAsync(id);
        if (order == null) throw new InvalidOperationException("Order not found.");

        order.Cancel();
        await _repository.SaveChangesAsync();
    }
}
