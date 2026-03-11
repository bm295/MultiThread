using LaCheminee.FnB.Domain.Entities;

namespace LaCheminee.FnB.Application.Ports;

public interface IKitchenNotifier
{
    Task NotifyOrderSentAsync(Order order, CancellationToken cancellationToken = default);
}
