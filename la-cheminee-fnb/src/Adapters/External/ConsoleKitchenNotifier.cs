using LaCheminee.FnB.Application.Ports;
using LaCheminee.FnB.Domain.Entities;

namespace LaCheminee.FnB.Adapters.External;

public sealed class ConsoleKitchenNotifier : IKitchenNotifier
{
    public Task NotifyOrderSentAsync(Order order, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"[Kitchen] Order #{order.Id} sent with {order.Items.Count} item(s).");
        return Task.CompletedTask;
    }
}
