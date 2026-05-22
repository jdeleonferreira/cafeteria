using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafeteria.Domain.Inventory;

namespace Cafeteria.Application.Inventory;

public class InventoryService
{
    private readonly IInventoryRepository _repository;

    public InventoryService(IInventoryRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Guid> CreateAsync(CreateInventoryItemRequest request)
    {
        var item = new InventoryItem(request.IngredientOptionId, request.InitialQuantity, request.UnitOfMeasure, request.ReorderThreshold, request.CreatedAtUtc, request.CreatedByUserId);

        await _repository.AddAsync(item);
        await _repository.SaveChangesAsync();

        return item.Id;
    }

    public async Task AdjustAsync(Guid inventoryItemId, decimal newQuantity, string reason, DateTime createdAtUtc, Guid? createdBy)
    {
        var item = await _repository.GetByIdAsync(inventoryItemId);
        if (item == null) throw new InvalidOperationException("Inventory item not found.");

        item.AdjustQuantity(newQuantity, reason, createdAtUtc, createdBy);
        await _repository.SaveChangesAsync();
    }

    public async Task<InventoryDto?> GetByIdAsync(Guid id)
    {
        var item = await _repository.GetByIdAsync(id);
        if (item == null) return null;

        return new InventoryDto(item.Id, item.IngredientOptionId, item.QuantityOnHand, item.UnitOfMeasure, item.ReorderThreshold);
    }

    public async Task<IReadOnlyList<InventoryDto>> ListAsync()
    {
        var items = await _repository.ListAsync();
        return items.Select(i => new InventoryDto(i.Id, i.IngredientOptionId, i.QuantityOnHand, i.UnitOfMeasure, i.ReorderThreshold)).ToList();
    }
}
