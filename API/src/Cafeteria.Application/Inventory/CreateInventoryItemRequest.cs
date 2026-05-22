using System;
using Cafeteria.Domain.Enums;

namespace Cafeteria.Application.Inventory;

public record CreateInventoryItemRequest(Guid IngredientOptionId, decimal InitialQuantity, UnitOfMeasure UnitOfMeasure, decimal ReorderThreshold, DateTime CreatedAtUtc, Guid? CreatedByUserId);
