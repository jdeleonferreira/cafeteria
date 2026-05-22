using System;
using System.Collections.Generic;

namespace Cafeteria.Application.Orders;

public record OrderDto(Guid Id, int OrderNumber, string CustomerName, decimal DonationAmount, decimal Total, string Status, DateTime CreatedAtUtc, IReadOnlyCollection<OrderItemDto> Items);
