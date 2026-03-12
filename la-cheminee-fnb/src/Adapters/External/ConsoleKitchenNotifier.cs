using System.Threading.Channels;
using LaCheminee.FnB.Application.Ports;
using LaCheminee.FnB.Domain.Entities;

namespace LaCheminee.FnB.Adapters.External;

public sealed class ConsoleKitchenNotifier : IKitchenNotifier, IDisposable
{
    private readonly Channel<KitchenDispatchTicket> _dispatchQueue = Channel.CreateUnbounded<KitchenDispatchTicket>(
        new UnboundedChannelOptions
        {
            SingleReader = true,
            SingleWriter = false
        });
    private readonly Task _dispatchWorker;

    public ConsoleKitchenNotifier()
    {
        _dispatchWorker = ProcessQueueAsync();
    }

    public Task NotifyOrderSentAsync(Order order, CancellationToken cancellationToken = default) =>
        _dispatchQueue.Writer.WriteAsync(
            new KitchenDispatchTicket(order.Id, order.TableNumber, order.Items.Count, DateTimeOffset.UtcNow),
            cancellationToken).AsTask();

    public void Dispose()
    {
        _dispatchQueue.Writer.TryComplete();
        _dispatchWorker.GetAwaiter().GetResult();
    }

    private async Task ProcessQueueAsync()
    {
        await foreach (var ticket in _dispatchQueue.Reader.ReadAllAsync())
        {
            await Task.Delay(75);
            Console.WriteLine(
                $"[Kitchen] Dispatching order #{ticket.OrderId} for table {ticket.TableNumber} with {ticket.ItemCount} item(s), queued at {ticket.QueuedAt:HH:mm:ss}.");
        }
    }

    private readonly record struct KitchenDispatchTicket(
        int OrderId,
        int TableNumber,
        int ItemCount,
        DateTimeOffset QueuedAt);
}
