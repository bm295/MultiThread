using LaCheminee.FnB.Domain.Entities;

namespace LaCheminee.FnB.Application.Ports;

public interface ITableRepository
{
    Task SeedLayout150SeatsAsync(CancellationToken cancellationToken = default);
    Task<Table> GetAsync(int tableNumber, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Table>> ListAsync(CancellationToken cancellationToken = default);
}
