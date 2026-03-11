using LaCheminee.FnB.Application.Models;
using LaCheminee.FnB.Application.Ports;
using LaCheminee.FnB.Domain.Entities;
using LaCheminee.FnB.Domain.Enums;

namespace LaCheminee.FnB.Adapters.External;

public sealed class SimulatedPaymentGateway : IPaymentGateway
{
    public Task<PaymentTransaction> ProcessAsync(PaymentRequest request, CancellationToken cancellationToken = default)
    {
        if (request.Amount <= 0)
        {
            return Task.FromResult(new PaymentTransaction(
                Guid.NewGuid().ToString("N"),
                request.OrderId,
                request.Amount,
                request.Method,
                PaymentStatus.Failed,
                DateTimeOffset.UtcNow));
        }

        return Task.FromResult(new PaymentTransaction(
            Guid.NewGuid().ToString("N"),
            request.OrderId,
            request.Amount,
            request.Method,
            PaymentStatus.Captured,
            DateTimeOffset.UtcNow));
    }
}
