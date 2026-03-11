namespace LaCheminee.FnB.Domain.Services;

public static class BillingCalculator
{
    public static (decimal Subtotal, decimal ServiceCharge, decimal Vat, decimal Total) Calculate(
        decimal subtotal,
        decimal serviceChargeRate,
        decimal vatRate)
    {
        var serviceCharge = subtotal * serviceChargeRate;
        var vat = (subtotal + serviceCharge) * vatRate;
        var total = subtotal + serviceCharge + vat;

        return (
            decimal.Round(subtotal, 2),
            decimal.Round(serviceCharge, 2),
            decimal.Round(vat, 2),
            decimal.Round(total, 2));
    }
}
