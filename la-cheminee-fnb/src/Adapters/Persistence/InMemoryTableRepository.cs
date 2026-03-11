using LaCheminee.FnB.Application.Ports;
using LaCheminee.FnB.Domain.Entities;

namespace LaCheminee.FnB.Adapters.Persistence;

public sealed class InMemoryTableRepository : ITableRepository
{
    private readonly Dictionary<int, Table> _tables = [];

    public Task SeedLayout150SeatsAsync(CancellationToken cancellationToken = default)
    {
        var distribution = new Dictionary<string, int[]>
        {
            ["Indoor"] = Enumerable.Repeat(4, 16).ToArray(),
            ["Terrace"] = Enumerable.Repeat(2, 10).ToArray(),
            ["Private"] = Enumerable.Repeat(6, 11).ToArray()
        };

        _tables.Clear();
        var tableNo = 1;
        foreach (var (area, capacities) in distribution)
        {
            foreach (var capacity in capacities)
            {
                _tables[tableNo] = new Table(tableNo, area, capacity);
                tableNo++;
            }
        }

        return Task.CompletedTask;
    }

    public Task<Table> GetAsync(int tableNumber, CancellationToken cancellationToken = default)
    {
        if (!_tables.TryGetValue(tableNumber, out var table))
        {
            throw new KeyNotFoundException($"Table {tableNumber} not found.");
        }

        return Task.FromResult(table);
    }

    public Task<IReadOnlyCollection<Table>> ListAsync(CancellationToken cancellationToken = default) =>
        Task.FromResult<IReadOnlyCollection<Table>>(_tables.Values.ToList());
}
