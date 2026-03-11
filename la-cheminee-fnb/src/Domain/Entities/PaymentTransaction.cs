using LaCheminee.FnB.Domain.Enums;

namespace LaCheminee.FnB.Domain.Entities;

public sealed record PaymentTransaction(
    string TransactionId,
    int OrderId,
    decimal Amount,
    string Method,
    PaymentStatus Status,
    DateTimeOffset ProcessedAt);
