using Cafeteria.Domain.Common;
using Cafeteria.Domain.Enums;

namespace Cafeteria.Domain.Orders;

public class Order : AggregateRoot
{
    private readonly List<OrderItem> _items = new List<OrderItem>();

    public int OrderNumber { get; private set; }
    public string CustomerName { get; private set; }
    public decimal DonationAmount { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    private Order()
    {
        CustomerName = string.Empty;
    }

    public Order(int orderNumber, string customerName, decimal donationAmount, DateTime createdAtUtc)
    {
        if (orderNumber <= 0)
            throw new ArgumentException("Order number must be greater than zero.", nameof(orderNumber));

        if (string.IsNullOrWhiteSpace(customerName))
            throw new ArgumentException("Customer name is required.", nameof(customerName));

        if (donationAmount < 0)
            throw new ArgumentException("Donation amount cannot be negative.", nameof(donationAmount));

        OrderNumber = orderNumber;
        CustomerName = customerName.Trim();
        DonationAmount = donationAmount;
        CreatedAtUtc = createdAtUtc;
        Status = OrderStatus.Confirmed;
    }

    public void AddItem(Guid productId, string productName, string sizeName, int quantity, decimal unitPrice)
    {
        _items.Add(new OrderItem(Id, productId, productName, sizeName, quantity, unitPrice));
    }

    public decimal GetTotal()
    {
        var itemsTotal = _items.Sum(i => i.GetLineTotal());
        return itemsTotal + DonationAmount;
    }

    public void MarkInPreparation()
    {
        if (Status != OrderStatus.Confirmed)
            throw new InvalidOperationException("Only confirmed orders can move to preparation.");

        Status = OrderStatus.InPreparation;
    }

    public void MarkReady()
    {
        if (Status != OrderStatus.InPreparation)
            throw new InvalidOperationException("Only orders in preparation can be marked as ready.");

        Status = OrderStatus.Ready;
    }

    public void MarkDelivered()
    {
        if (Status != OrderStatus.Ready)
            throw new InvalidOperationException("Only ready orders can be delivered.");

        Status = OrderStatus.Delivered;
    }

    public void Cancel()
    {
        if (Status is OrderStatus.Delivered)
            throw new InvalidOperationException("Delivered orders cannot be cancelled.");

        Status = OrderStatus.Cancelled;
    }
}
