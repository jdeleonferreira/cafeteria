using System;

namespace Cafeteria.Application.Orders;

public record OrderItemDto(Guid Id, Guid ProductId, string ProductName, string SizeName, int Quantity, decimal UnitPrice, decimal LineTotal);
