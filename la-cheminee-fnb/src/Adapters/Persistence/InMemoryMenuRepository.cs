using LaCheminee.FnB.Application.Ports;
using LaCheminee.FnB.Domain.Entities;

namespace LaCheminee.FnB.Adapters.Persistence;

public sealed class InMemoryMenuRepository : IMenuRepository
{
    private readonly Dictionary<string, MenuItem> _items = new(StringComparer.OrdinalIgnoreCase);

    public Task AddAsync(MenuItem item, CancellationToken cancellationToken = default)
    {
        if (item.Price <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(item.Price), "Price must be greater than zero.");
        }

        _items[item.Code] = item;
        return Task.CompletedTask;
    }

    public Task<MenuItem> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        if (!_items.TryGetValue(code, out var item))
        {
            throw new KeyNotFoundException($"Menu item '{code}' not found.");
        }

        return Task.FromResult(item);
    }
}
