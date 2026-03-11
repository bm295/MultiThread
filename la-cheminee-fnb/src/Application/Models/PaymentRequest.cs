namespace LaCheminee.FnB.Application.Models;

public sealed record PaymentRequest(int OrderId, decimal Amount, string Method);
