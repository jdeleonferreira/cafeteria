using System;
using Cafeteria.Domain.Enums;

namespace Cafeteria.Application.Inventory;

public record InventoryDto(Guid Id, Guid IngredientOptionId, decimal QuantityOnHand, UnitOfMeasure UnitOfMeasure, decimal ReorderThreshold);
