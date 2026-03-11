using LaCheminee.FnB.Application.Ports;
using LaCheminee.FnB.Domain.Entities;

namespace LaCheminee.FnB.Adapters.Persistence;

public sealed class InMemoryInventoryRepository : IInventoryRepository
{
    private readonly Dictionary<string, InventoryItem> _stock = new(StringComparer.OrdinalIgnoreCase)
    {
        ["A01"] = new InventoryItem("A01", 100),
        ["M01"] = new InventoryItem("M01", 100),
        ["D01"] = new InventoryItem("D01", 100)
    };

    public Task<InventoryItem> GetByMenuCodeAsync(string menuCode, CancellationToken cancellationToken = default)
    {
        if (!_stock.TryGetValue(menuCode, out var item))
        {
            throw new KeyNotFoundException($"Inventory for menu code '{menuCode}' not found.");
        }

        return Task.FromResult(item);
    }

    public Task SaveAsync(InventoryItem item, CancellationToken cancellationToken = default)
    {
        _stock[item.MenuCode] = item;
        return Task.CompletedTask;
    }
}
