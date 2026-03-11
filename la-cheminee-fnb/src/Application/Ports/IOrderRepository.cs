using LaCheminee.FnB.Domain.Entities;

namespace LaCheminee.FnB.Application.Ports;

public interface IOrderRepository
{
    Task<Order> CreateAsync(int tableNumber, string staff, CancellationToken cancellationToken = default);
    Task<Order> GetAsync(int orderId, CancellationToken cancellationToken = default);
    Task SaveAsync(Order order, CancellationToken cancellationToken = default);
}
