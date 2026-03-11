using LaCheminee.FnB.Domain.Entities;

namespace LaCheminee.FnB.Application.Ports;

public interface IInventoryRepository
{
    Task<InventoryItem> GetByMenuCodeAsync(string menuCode, CancellationToken cancellationToken = default);
    Task SaveAsync(InventoryItem item, CancellationToken cancellationToken = default);
}
