using System;
using System.Collections.Generic;

namespace Cafeteria.Application.Orders;

public record CreateOrderItemRequest(Guid ProductId, string ProductName, string SizeName, int Quantity, decimal UnitPrice);

public record CreateOrderRequest(int OrderNumber, string CustomerName, decimal DonationAmount, DateTime CreatedAtUtc, IReadOnlyCollection<CreateOrderItemRequest> Items);
