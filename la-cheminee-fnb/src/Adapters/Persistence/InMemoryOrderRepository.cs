using LaCheminee.FnB.Application.Ports;
using LaCheminee.FnB.Domain.Entities;

namespace LaCheminee.FnB.Adapters.Persistence;

public sealed class InMemoryOrderRepository : IOrderRepository
{
    private readonly Dictionary<int, Order> _orders = [];
    private int _nextOrderId = 1;

    public Task<Order> CreateAsync(int tableNumber, string staff, CancellationToken cancellationToken = default)
    {
        var order = new Order(_nextOrderId++, tableNumber, staff);
        _orders[order.Id] = order;
        return Task.FromResult(order);
    }

    public Task<Order> GetAsync(int orderId, CancellationToken cancellationToken = default)
    {
        if (!_orders.TryGetValue(orderId, out var order))
        {
            throw new KeyNotFoundException($"Order {orderId} not found.");
        }

        return Task.FromResult(order);
    }

    public Task SaveAsync(Order order, CancellationToken cancellationToken = default)
    {
        _orders[order.Id] = order;
        return Task.CompletedTask;
    }
}
