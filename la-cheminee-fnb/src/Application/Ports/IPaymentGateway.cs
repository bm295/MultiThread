using LaCheminee.FnB.Application.Models;
using LaCheminee.FnB.Domain.Entities;

namespace LaCheminee.FnB.Application.Ports;

public interface IPaymentGateway
{
    Task<PaymentTransaction> ProcessAsync(PaymentRequest request, CancellationToken cancellationToken = default);
}
