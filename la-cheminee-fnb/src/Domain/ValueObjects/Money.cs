namespace LaCheminee.FnB.Domain.ValueObjects;

public readonly record struct Money(decimal Amount)
{
    public static Money Zero => new(0m);

    public static Money operator +(Money left, Money right) => new(left.Amount + right.Amount);
    public static Money operator -(Money left, Money right) => new(left.Amount - right.Amount);
    public static Money operator *(Money left, decimal scalar) => new(left.Amount * scalar);

    public Money Round(int decimals = 2) => new(decimal.Round(Amount, decimals));

    public override string ToString() => Round().Amount.ToString("0.00");
}
