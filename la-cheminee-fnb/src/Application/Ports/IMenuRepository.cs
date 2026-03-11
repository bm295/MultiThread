using LaCheminee.FnB.Domain.Entities;

namespace LaCheminee.FnB.Application.Ports;

public interface IMenuRepository
{
    Task AddAsync(MenuItem item, CancellationToken cancellationToken = default);
    Task<MenuItem> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
}
